using Cinema.Data.Features.Prices.Commands.CreatePrice;
using Cinema.Data.Features.Prices.Commands.DeletePrice;
using Cinema.Data.Features.Prices.Queries.GetPrices;
using Cinema.Data.Features.Prices.Queries.GetPricesForList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/prices")]
[Authorize(Roles = "admin")]
public sealed class PricesController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetPricesAsync(
    [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetPricesQuery(), cancellationToken));
    }
    //[HttpGet]
    //[Route("a/{priceListId:int}")]
    //public async Task<IActionResult> GetPricesForListAsync(
    //    [FromRoute] int priceListId,
    //    [FromQuery] CancellationToken cancellationToken)
    //{
    //    return Ok(await Mediator.Send(new GetPricesForListQuery(priceListId), cancellationToken));
    //}

    //[HttpGet("{movieId:int}", Name = nameof(GetMovieAsync))]
    //public async Task<IActionResult> GetMovieAsync(
    //    [FromRoute] int movieId,
    //    [FromQuery] CancellationToken cancellationToken)
    //{
    //    return Ok(await Mediator.Send(new GetPriceListQuery(movieId), cancellationToken));
    //}

    [HttpPost]
    public async Task<IActionResult> CreatePriceAsync(
        [FromBody] CreatePriceDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new CreatePriceCommand(dto),
            cancellationToken);

        return Ok();
    }
    [HttpDelete("{priceId:int}")]
    public async Task<IActionResult> DeletePriceAsync(
    [FromRoute] int priceId,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeletePriceCommand(priceId),
            cancellationToken);

        return NoContent();
    }
}