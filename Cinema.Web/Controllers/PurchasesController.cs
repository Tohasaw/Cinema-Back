using Cinema.Data.Features.Purchases.Commands.UpdatePurchase;
using Cinema.Data.Features.Purchases.Queries.GetPurchase;
using Cinema.Data.Features.Purchases.Queries.GetPurchases;
using Cinema.Data.Features.SeatPrices.Commands.CreatePurchase;
using Cinema.Data.Features.TableEntries.Commands.DeletePurchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/purchases")]
[Authorize(Roles = "admin,cassir,controler")]
public sealed class PurchasesController : Controller
{
    [HttpGet("{purchaseId:int}")]
    public async Task<IActionResult> GetPurchaseAsync(
        [FromRoute] int purchaseId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetPurchaseQuery(purchaseId), cancellationToken));
    }
    [HttpGet]
    public async Task<IActionResult> GetPurchasesAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetPurchasesQuery(), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePurchaseAsync(
        [FromBody] CreatePurchaseDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var movieId = await Mediator.Send(
            new CreatePurchaseCommand(dto),
            cancellationToken);

        return Ok();
    }

    [HttpPut("{purchaseId:int}")]
    public async Task<IActionResult> UpdatePurchaseAsync(
        [FromRoute] int purchaseId,
        [FromBody] UpdatePurchaseDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdatePurchaseCommand(purchaseId, dto),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{purchaseId:int}")]
    public async Task<IActionResult> DeleteMovieAsync(
    [FromRoute] int purchaseId,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeletePurchaseCommand(purchaseId),
            cancellationToken);

        return NoContent();
    }
}