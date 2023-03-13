using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Movies.Commands.DeleteMovie;
public sealed record DeleteMovieCommand(int MovieId) : IRequest;

internal sealed class DeleteMovieHandler : AsyncRequestHandler<DeleteMovieCommand>
{
    private readonly DbContext _context;

    public DeleteMovieHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeleteMovieCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsMovieAsync(request.MovieId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующего фильма!");

        var note = await GetMovieAsync(request.MovieId, cancellationToken);

        _context.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsExistsMovieAsync(
        int movieId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Movie>()
            .AnyAsync(n => n.Id == movieId, cancellationToken);

        return exists;
    }

    private async Task<Movie> GetMovieAsync(
        int movieId,
        CancellationToken cancellationToken)
    {
        var note = await _context
            .Set<Movie>()
            .SingleAsync(n => n.Id == movieId, cancellationToken);

        return note;
    }
}
