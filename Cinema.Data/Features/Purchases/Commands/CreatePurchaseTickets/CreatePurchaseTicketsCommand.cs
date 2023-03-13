using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.JwtTokenServices;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Purchases.Commands.CreatePurchaseTickets
{
    public sealed record CreatePurchaseTicketsCommand(CreatePurchaseTicketsDto Dto) : IRequest<string>;

    public sealed class CreatePurchaseTicketsHandler : IRequestHandler<CreatePurchaseTicketsCommand, string>
    {
        public CreatePurchaseTicketsHandler(
            DbContext context,
            IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;
        }
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public async Task<string> Handle(
            CreatePurchaseTicketsCommand request,
            CancellationToken cancellationToken)
        {
            var entryExists = await IsEntryExists(
                request.Dto.TableEntryId,
                cancellationToken);
            if (!entryExists)
                throw new BadRequestException("Позиции расписания не существует");

            var purchase = new Purchase()
            {
                TableEntryId = request.Dto.TableEntryId,
                EmailAddress = request.Dto.EmailAddress,
                PhoneNumber = request.Dto.PhoneNumber,
                AdvertAccepted = request.Dto.AdvertAccepted,
                PriceTotal = 0,
                DateTime = DateTime.Now,
                RefundKey = Guid.NewGuid().ToString(),
            };
            await _context.AddAsync(purchase, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            double totalPrice = 0;
            foreach (var seatId in request.Dto.SeatIds)
            {
                var exists = await IsExistsTicketAsync(
                    purchase.TableEntryId,
                    seatId,
                    cancellationToken);
                if (exists)
                    throw new BadRequestException("Билет на выбранное место уже куплен");

                var price = await GetPriceAsync(purchase.TableEntryId, seatId, cancellationToken);
                totalPrice += price;
                var ticket = new Ticket()
                {
                    SeatId = seatId,
                    PurchaseId = purchase.Id,
                    Cancelled = false,
                    Visited = false,
                    Key = Guid.NewGuid().ToString(),
                    Price = price,
                };

                await _context.AddAsync(ticket, cancellationToken);
            }
            purchase.PriceTotal = totalPrice;
            await _context.SaveChangesAsync(cancellationToken);
            return purchase.RefundKey;
        }
        private async Task<bool> IsEntryExists(
            int tableEntryId,
            CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<TableEntry>()
                .AsNoTracking()
                .AnyAsync(t => t.Id == tableEntryId, cancellationToken);

            return exists;
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
            var tableEntry = await _context
                .Set<TableEntry>()
                .AsNoTracking()
                .SingleAsync(t => t.Id == entryId);

            var seatPrice = await _context
                .Set<SeatPrice>()
                .AsNoTracking()
                .Include(p => p.Price)
                .SingleAsync(p => p.PriceListId == tableEntry.PriceListId && p.SeatId == seatId, cancellationToken);

            return seatPrice.Price.Value;
        }
    }
}
