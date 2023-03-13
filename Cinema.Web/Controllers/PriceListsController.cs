using Cinema.Data.Features.PriceLists.Commands.CreatePriceList;
using Cinema.Data.Features.PriceLists.Commands.DeletePriceList;
using Cinema.Data.Features.PriceLists.Commands.UpdatePriceList;
using Cinema.Data.Features.PriceLists.Queries.GetPriceLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/pricelists")]
[Authorize(Roles = "admin")]
public sealed class PriceListsController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetPriceListsAsync(
    [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetPriceListsQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePriceListAsync(
        [FromBody] CreatePriceListDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var movieId = await Mediator.Send(
            new CreatePriceListCommand(dto),
            cancellationToken);

        return Ok();
    }
    [HttpPut("{priceListId:int}")]
    public async Task<IActionResult> UpdateMovieAsync(
    [FromRoute] int priceListId,
    [FromBody] UpdatePriceListDto dto,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdatePriceListCommand(priceListId, dto),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{priceListId:int}")]
    public async Task<IActionResult> DeleteMovieAsync(
    [FromRoute] int priceListId,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeletePriceListCommand(priceListId),
            cancellationToken);

        return NoContent();
    }
}