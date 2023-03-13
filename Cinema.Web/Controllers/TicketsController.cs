using Cinema.Data.Features.TableEntries.Commands.DeleteTicket;
using Cinema.Data.Features.Tickets.Commands.CreateTicket;
using Cinema.Data.Features.Tickets.Commands.UpdateTicket;
using Cinema.Data.Features.Tickets.Queries.GetTickets;
using Cinema.Data.Features.Tickets.Queries.GetTicketsByPurchase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cinema.Web.Controllers;

[Route("api/tickets")]
public sealed class TicketsController : Controller
{

    // Получить информацию о свободных местах
    [HttpGet]
    [Authorize(Roles = "admin,cassir,controler")]
    public async Task<IActionResult> GetTicketsAsync(
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTicketsQuery(), cancellationToken));
    }
    [HttpGet]
    [Authorize(Roles = "admin,cassir,controler")]
    [Route("{purchaseId:int}")]
    public async Task<IActionResult> GetTicketsByPurchaseAsync(
        [FromRoute] int purchaseId,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTicketsByPurchaseQuery(purchaseId), cancellationToken));
    }
    [HttpPost]
    [Authorize(Roles = "admin,cassir")]
    public async Task<IActionResult> CreateTicketAsync(
        [FromBody] CreateTicketDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new CreateTicketCommand(dto),
            cancellationToken);

        return Ok();
    }
    [HttpPut("{ticketId:int}")]
    [Authorize(Roles = "admin,cassir")]
    public async Task<IActionResult> UpdatePurchaseAsync(
    [FromRoute] int ticketId,
    [FromBody] UpdateTicketDto dto,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new UpdateTicketCommand(ticketId, dto),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{ticketId:int}")]
    [Authorize(Roles = "admin,cassir")]
    public async Task<IActionResult> DeleteMovieAsync(
    [FromRoute] int ticketId,
    [FromQuery] CancellationToken cancellationToken)
    {
        await Mediator.Send(
            new DeleteTicketCommand(ticketId),
            cancellationToken);

        return NoContent();
    }
}

public class Data
{
    public IEnumerable<CreateTicketDto> data { get; set; }
}
public class CustomConverter : JsonConverter<List<CreateTicketDto>>
{
    public override List<CreateTicketDto> ReadJson(JsonReader reader, Type objectType, List<CreateTicketDto> existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);
        if (token.Type == JTokenType.Array)
        {
            return token.ToObject<List<CreateTicketDto>>();
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, List<CreateTicketDto> value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}