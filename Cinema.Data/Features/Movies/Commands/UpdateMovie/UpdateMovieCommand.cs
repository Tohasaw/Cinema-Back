using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Movies.Commands.UpdateMovie;
public sealed record UpdateMovieCommand(int MovieId, UpdateMovieDto Dto) : IRequest;

internal sealed class UpdateMovieHandler : AsyncRequestHandler<UpdateMovieCommand>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    public UpdateMovieHandler(
        DbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    protected override async Task Handle(
        UpdateMovieCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsMovieAsync(
            request.MovieId,
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую заметку");

        var movie = await GetMovieAsync(
            request.MovieId,
            cancellationToken);

        _mapper.Map(request.Dto, movie);
        await _context.SaveChangesAsync(cancellationToken);
    }
    private async Task<bool> IsExistsMovieAsync(int movieId, CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Movie>()
            .AsNoTracking()
            .AnyAsync(m => m.Id == movieId, cancellationToken);

        return exists;
    }
    private async Task<Movie> GetMovieAsync(int movieId, CancellationToken cancellationToken)
    {
        var movie = await _context
        .Set<Movie>()
        .AsTracking()
        .SingleAsync(m => m.Id == movieId, cancellationToken);

        return movie;
    }
}
