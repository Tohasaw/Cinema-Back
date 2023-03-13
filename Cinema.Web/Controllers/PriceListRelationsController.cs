using Cinema.Data.Features.PriceListRelations.Commands.CreatePriceListRelation;
using Cinema.Data.Features.PriceLists.Commands.CreatePriceList;
using Cinema.Data.Features.PriceLists.Queries.GetPriceLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/pricelistrelations")]
[Authorize(Roles = "admin")]
public sealed class PriceListRelationsController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetPriceListRelationAsync(
    [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetPriceListsQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePriceListRelationAsync(
        [FromBody] CreatePriceListRelationDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new CreatePriceListRelationCommand(dto),
            cancellationToken);

        return Ok();
    }
}