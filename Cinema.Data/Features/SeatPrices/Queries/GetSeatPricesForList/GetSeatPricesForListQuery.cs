using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForList
{
    public sealed record GetSeatPricesForListQuery(int PriceListId) : IRequest<IEnumerable<GetSeatPricesForListDto>>;
    internal sealed class GetSeatPricesForListHandler : IRequestHandler<GetSeatPricesForListQuery, IEnumerable<GetSeatPricesForListDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetSeatPricesForListHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetSeatPricesForListDto>> Handle(
            GetSeatPricesForListQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request.PriceListId, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetSeatPricesForListDto>> GetDtoAsync(
            int priceListId,
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<SeatPrice>()
               .Include(s => s.Price)
               .Where(s => s.PriceListId == priceListId)
               .ProjectTo<GetSeatPricesForListDto>(_provider)
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
