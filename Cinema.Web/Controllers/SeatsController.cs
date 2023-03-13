using Cinema.Data.Features.Seats.Commands.CreateSeat;
using Cinema.Data.Features.Seats.Commands.UpdateSeats;
using Cinema.Data.Features.Seats.Queries.GetSeats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/seats")]
[Authorize(Roles = "admin")]
public sealed class SeatsController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetSeatsAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSeatsQuery(), cancellationToken));
    }

    //[HttpGet("{movieId:int}", Name = nameof(GetMovieAsync))]
    //public async Task<IActionResult> GetMovieAsync(
    //    [FromRoute] int movieId,
    //    [FromQuery] CancellationToken cancellationToken)
    //{
    //    return Ok(await Mediator.Send(new GetMovieQuery(movieId), cancellationToken));
    //}

    [HttpPost]
    public async Task<IActionResult> CreateSeatAsync(
        [FromBody] CreateSeatDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var seatId = await Mediator.Send(
            new CreateSeatCommand(dto),
            cancellationToken);

        //return CreatedAtRoute(
        //    // ReSharper disable once Mvc.ActionNotResolved
        //    nameof(GetMovieAsync),
        //    new
        //    {
        //        movieId
        //    },
        //    movieId);
        return Ok(seatId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSeatsAsync(
        [FromBody] int[] SeatIds,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdateSeatsCommand(SeatIds),
            cancellationToken);

        return NoContent();
    }

    //[HttpDelete("{movieId:int}")]
    //public async Task<IActionResult> DeleteNoteAsync(
    //[FromRoute] int movieId,
    //[FromQuery] CancellationToken cancellationToken)
    //{
    //    await Mediator.Send(
    //        new DeleteMovieCommand(movieId),
    //        cancellationToken);

    //    return NoContent();
    //}
}

