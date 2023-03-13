using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Tickets.Commands.UpdateTicket;
public sealed record UpdateTicketCommand(int TicketId, UpdateTicketDto Dto) : IRequest;

internal sealed class UpdateTicketHandle : AsyncRequestHandler<UpdateTicketCommand>
{
    private readonly IMapper _mapper;
    private readonly DbContext _context;

    public UpdateTicketHandle(
        IMapper mapper,
        DbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    protected override async Task Handle(
        UpdateTicketCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExisting(
            request.TicketId,
            cancellationToken);

        if (!exists)
            throw new BadRequestException("Попытка обновить несуществующую запись");

        var ticket = await GetTicket(
            request.TicketId,
            cancellationToken);

        _mapper.Map(request.Dto, ticket);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Ticket> GetTicket(int ticketId, CancellationToken cancellationToken)
    {
        var Ticket = await _context
            .Set<Ticket>()
            .SingleAsync(t => t.Id == ticketId, cancellationToken);

        return Ticket;
    }

    private async Task<bool> IsExisting(int ticketId, CancellationToken cancellationToken)
    {
        var exist = await _context
            .Set<Ticket>()
            .AnyAsync(t => t.Id == ticketId, cancellationToken);

        return exist;
    }
}

