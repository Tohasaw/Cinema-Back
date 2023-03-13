using Cinema.Data.Features.Tickets.Commands.MarkTicketVisited;
using Cinema.Data.Features.Tickets.Queries.GetTicketByKey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;

[Route("api/ticketcheck")]
[Authorize(Roles = "admin,cassir,controler")]
public sealed class TicketCheckController : Controller
{

    // Получить информацию о свободных местах
    [HttpGet("{ticketKey}")]
    public async Task<IActionResult> GetTicket(
        [FromRoute] string ticketKey,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetTicketByKeyQuery(ticketKey), cancellationToken));
    }


    //Купить билет или билеты (сделать проверку на купленные места)
    [HttpPost("{ticketKey}")]
    public async Task<IActionResult> MarkTicketVisited(
        [FromRoute] string ticketKey,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new MarkTicketVisitedCommand(ticketKey), cancellationToken));
    }
}