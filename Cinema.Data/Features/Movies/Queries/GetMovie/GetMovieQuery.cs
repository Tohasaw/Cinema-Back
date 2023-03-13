using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Movies.Queries.GetMovie;

public sealed record GetMovieQuery : IRequest<GetMovieDto>
{
    public GetMovieQuery(int movieId)
    {
        MovieId = movieId;
    }

    public int MovieId { get; }
}

public sealed class GetMovieHandler : IRequestHandler<GetMovieQuery, GetMovieDto>
{
    private readonly DbContext _context;
    private readonly IConfigurationProvider _provider;

    public GetMovieHandler(
        DbContext context,
        IConfigurationProvider provider)
    {
        _context = context;
        _provider = provider;
    }

    public async Task<GetMovieDto> Handle(
        GetMovieQuery request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsMovieAsync(
            request.MovieId,
            cancellationToken);
        if (!exists)
            throw new NotFoundException("Фильм не найден");

        var dto = await GetDtoAsync(
            request.MovieId,
            cancellationToken);

        return dto;
    }

    private async Task<bool> IsExistsMovieAsync(
    int movieId,
    CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Movie>()
            .AnyAsync(movie => movie.Id == movieId, cancellationToken);

        return exists;
    }

    private async Task<GetMovieDto> GetDtoAsync(
        int movieId,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<Movie>()
            .Where(movie => movie.Id == movieId)
            .ProjectTo<GetMovieDto>(_provider)
            .SingleAsync(cancellationToken);
    }
}

