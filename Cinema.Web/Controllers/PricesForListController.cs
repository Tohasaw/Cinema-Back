using Cinema.Data.Features.PriceListRelations.Commands.CreatePriceListRelation;
using Cinema.Data.Features.PriceListRelations.Commands.DeletePriceListRelation;
using Cinema.Data.Features.Prices.Queries.GetPricesForList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers
{
    [Route("api/pricesforlist")]
    [Authorize(Roles = "admin")]
    public class PricesForListController : Controller
    {
        [HttpGet]
        [Route("{priceListId:int}")]
        public async Task<IActionResult> GetPricesForListAsync(
            [FromRoute] int priceListId,
            [FromQuery] CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetPricesForListQuery(priceListId), cancellationToken));
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
        [HttpDelete("{priceListId:int}")]
        public async Task<IActionResult> DeletePriceListRelationAsync(
            [FromRoute] int priceListId,
            [FromQuery] CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new DeletePriceListRelationCommand(priceListId),
                cancellationToken);

            return NoContent();
        }
    }
}
