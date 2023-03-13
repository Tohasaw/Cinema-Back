using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.TableEntries.Commands.DeleteTicket;
public sealed record DeleteTicketCommand(int TicketId) : IRequest;

internal sealed class DeleteTicketHandler: AsyncRequestHandler<DeleteTicketCommand>
{
    private readonly DbContext _context;

    public DeleteTicketHandler(
        DbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(
        DeleteTicketCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(request.TicketId, cancellationToken);
        if (!exists)
            throw new BadRequestException("Попытка удаления не существующей записи!");

        var ticket = await GetEntry(request.TicketId, cancellationToken);
        _context.Remove(ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Ticket> GetEntry(int ticketId, CancellationToken cancellationToken)
    {
        var Ticket = await _context
            .Set<Ticket>()
            .SingleAsync(e => e.Id == ticketId, cancellationToken);

        return Ticket;
    }

    private async Task<bool> IsExisting(
        int ticketId,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<Ticket>()
            .AnyAsync(e => e.Id == ticketId, cancellationToken);

        return exists;
    }
}

