using Cinema.Data.Features.SeatInfos.Queries.GetSeatInfos;
using Cinema.Data.Features.SeatInfos.Queries.GetSeatInfosFilt;
using Cinema.Data.Features.SeatPrices.Commands.CreateSeatPrices;
using Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/seatpricesforlist")]
[Authorize(Roles = "admin")]
public sealed class SeatPricesForListController : Controller
{
    [HttpGet("{priceListId:int}")]
    public async Task<IActionResult> GetSeatPricesForListAsync(
        [FromRoute] int priceListId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetSeatPricesForListQuery(priceListId), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeatPricesAsync(
        [FromBody] IEnumerable<CreateSeatPricesDto> dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreateSeatPricesCommand(dto), cancellationToken));
    }
}

