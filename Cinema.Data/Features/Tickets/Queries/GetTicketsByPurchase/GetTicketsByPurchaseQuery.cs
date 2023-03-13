using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using Cinema.Data.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Tickets.Queries.GetTicketsByPurchase
{
    public sealed record GetTicketsByPurchaseQuery(int PurchaseId) : IRequest<IEnumerable<GetTicketsByPurchaseDto>>;
    internal sealed class GetTicketsByPurchaseHandler : IRequestHandler<GetTicketsByPurchaseQuery, IEnumerable<GetTicketsByPurchaseDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetTicketsByPurchaseHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetTicketsByPurchaseDto>> Handle(
            GetTicketsByPurchaseQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request.PurchaseId ,cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetTicketsByPurchaseDto>> GetDtoAsync(
            int purchaseId,
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<Ticket>()
               .Include(t => t.Purchase)
               .Where(t => t.Purchase.Id == purchaseId)
               //.Include(t => t.SeatInfo)
               //.Include(t => t.SeatInfo.Seat)
               .ProjectTo<GetTicketsByPurchaseDto>(_provider)
               .OrderBy(t => t.Id)
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
