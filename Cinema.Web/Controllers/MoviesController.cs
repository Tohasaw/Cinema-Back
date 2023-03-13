using Cinema.Data.Features.Movies.Commands.CreateMovie;
using Cinema.Data.Features.Movies.Commands.DeleteMovie;
using Cinema.Data.Features.Movies.Commands.UpdateMovie;
using Cinema.Data.Features.Movies.Queries.GetMovie;
using Cinema.Data.Features.Movies.Queries.GetMovies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/movies")]
[Authorize(Roles = "admin")]
public sealed class MoviesController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetMoviesAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetMoviesQuery(), cancellationToken));
    }

    [HttpGet("{movieId:int}", Name = nameof(GetMovieAsync))]
    public async Task<IActionResult> GetMovieAsync(
        [FromRoute] int movieId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetMovieQuery(movieId), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovieAsync(
        [FromBody] CreateMovieDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var movieId = await Mediator.Send(
            new CreateMovieCommand(dto),
            cancellationToken);

        return CreatedAtRoute(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetMovieAsync),
            new
            {
                movieId
            },
            movieId);
    }

    [HttpPut("{movieId:int}")]
    public async Task<IActionResult> UpdateMovieAsync(
        [FromRoute] int movieId,
        [FromBody] UpdateMovieDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdateMovieCommand(movieId, dto),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{movieId:int}")]
    public async Task<IActionResult> DeleteMovieAsync(
    [FromRoute] int movieId,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeleteMovieCommand(movieId),
            cancellationToken);

        return NoContent();
    }
}

