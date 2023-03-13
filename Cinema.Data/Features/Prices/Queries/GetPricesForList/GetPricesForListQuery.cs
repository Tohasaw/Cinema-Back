using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Prices.Queries.GetPricesForList
{
    public sealed record GetPricesForListQuery(int PriceListId) : IRequest<IEnumerable<GetPricesForListDto>>;
    internal sealed class GetPricesForListHandler : IRequestHandler<GetPricesForListQuery, IEnumerable<GetPricesForListDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetPricesForListHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetPricesForListDto>> Handle(
            GetPricesForListQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetPricesForListDto>> GetDtoAsync(
            GetPricesForListQuery request,
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<Price>()
               .Where(p => p.PriceListRelations.Any(r => r.PriceListId == request.PriceListId))
               .ProjectTo<GetPricesForListDto>(_provider, new { priceListId = request.PriceListId })
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
