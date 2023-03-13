using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Tickets.Commands.MarkTicketVisited;
public sealed record MarkTicketVisitedCommand(string TicketKey) : IRequest;

internal sealed class MarkTicketVisitedHandle : AsyncRequestHandler<MarkTicketVisitedCommand>
{
    private readonly DbContext _context;

    public MarkTicketVisitedHandle(
        DbContext context)
    {
        _context = context;
    }
    protected override async Task Handle(
        MarkTicketVisitedCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await GetTicket(
            request.TicketKey,
            cancellationToken);
        if (ticket == null) throw new BadRequestException("Билет не найден.");

        if (ticket.Cancelled) throw new BadRequestException("Билет отменен.");
        else if (ticket.Visited) throw new BadRequestException("Билет уже использован.");
        else ticket.Visited = true;

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Ticket?> GetTicket(string ticketKey, CancellationToken cancellationToken)
    {
        var ticket = await _context
            .Set<Ticket>()
            .Where(x => x.Key == ticketKey)
            .SingleOrDefaultAsync(cancellationToken);

        return ticket;
    }
}

