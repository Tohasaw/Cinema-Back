using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Features.Purchases.Queries.GetPurchase;
using Cinema.Data.Services.JwtTokenServices;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Tickets.Commands.CreateTicket
{
    public sealed record CreateTicketCommand(CreateTicketDto Dto) : IRequest;

    public sealed class CreateTicketHandler : AsyncRequestHandler<CreateTicketCommand>
    {
        public CreateTicketHandler(
            DbContext context)
        {
            _context = context;
        }
        private readonly DbContext _context;

        protected override async Task Handle(
           CreateTicketCommand request,
            CancellationToken cancellationToken)
        {
            var purchase = await IsExistsPurchaseAsync(request.Dto.PurchaseId, cancellationToken);
             if (purchase == null)
                throw new BadRequestException("Покупки не существует");

            var exists = await IsExistsTicketAsync(
                purchase.TableEntryId,
                request.Dto.SeatId,
                cancellationToken);
            if (exists)
                throw new BadRequestException("Билет на выбранное место уже куплен");

            var price = await GetPriceAsync(purchase.TableEntryId, request.Dto.SeatId, cancellationToken);
            if (purchase.PriceTotal == null) purchase.PriceTotal = price;
            else purchase.PriceTotal += price;

            var ticket = new Ticket()
            {
                SeatId = request.Dto.SeatId,
                PurchaseId = purchase.Id,
                Cancelled = false,
                Visited = false,
                Price = price,
                Key = Guid.NewGuid().ToString(),
            };

            await _context.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Purchase?> IsExistsPurchaseAsync(int purchaseId, CancellationToken cancellationToken)
        {
            var purchase = await _context
             .Set<Purchase>()
             .SingleOrDefaultAsync(p => p.Id == purchaseId, cancellationToken);

            return purchase;
        }

        private async Task<bool> IsExistsTicketAsync(
            int entryId,
            int seatInfoId,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Purchase>()
                .Include(p => p.Tickets)
                .AsNoTracking()
                .AnyAsync(p => p.TableEntryId == entryId && p.Tickets.Any(t => t.SeatId == seatInfoId && t.Cancelled == false), cancellationToken);

            return exists;
        }
        private async Task<double> GetPriceAsync(
            int entryId,
            int seatId,
            CancellationToken cancellationToken)
        {
            var priceList = await _context
                .Set<TableEntry>()
                .AsNoTracking()
                .SingleAsync(t => t.Id == entryId);

            var seatPrice = await _context
                .Set<SeatPrice>()
                .Include(p => p.Price)
                .SingleAsync(p => p.PriceListId == priceList.PriceListId && p.SeatId == seatId, cancellationToken);
            return seatPrice.Price.Value;
        }

    }
}
