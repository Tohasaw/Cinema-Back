using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Tickets.Commands.CancelTickets;
public sealed record CancelTicketsCommand(CancelTicketsDto Dto) : IRequest;

internal sealed class CancelTicketsHandle : AsyncRequestHandler<CancelTicketsCommand>
{
    private readonly IMapper _mapper;
    private readonly DbContext _context;

    public CancelTicketsHandle(
        IMapper mapper,
        DbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    protected override async Task Handle(
        CancelTicketsCommand request,
        CancellationToken cancellationToken)
    {
        var purchase = await GetPurchase(
            request.Dto.RefundKey,
            cancellationToken);
        if (purchase == null)
            throw new BadRequestException("Покупка не найдена.");

        CancelTickets(
            request.Dto.TicketIds,
            purchase);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Purchase?> GetPurchase(string refundKey, CancellationToken cancellationToken)
    {
        var purchase = await _context
            .Set<Purchase>()
            .Include(x => x.Tickets)
            .SingleOrDefaultAsync(t => t.RefundKey == refundKey, cancellationToken);

        return purchase;
    }
    private void CancelTickets(int[] ticketIds, Purchase purchase)
    {
        foreach (var id in ticketIds)
        {
            var ticket = purchase.Tickets.SingleOrDefault(t => t.Id == id);
            if (ticket != null)
            {
                ticket.Cancelled = true;
            }
            else
            {
                throw new NotFoundException("Билет не найден.");
            }
        }
    }
}

