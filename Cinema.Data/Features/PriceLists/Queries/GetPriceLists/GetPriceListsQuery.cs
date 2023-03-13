using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.PriceLists.Queries.GetPriceLists
{
    public sealed record GetPriceListsQuery() : IRequest<IEnumerable<GetPriceListsDto>>;
    internal sealed class GetPriceListsHandler : IRequestHandler<GetPriceListsQuery, IEnumerable<GetPriceListsDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetPriceListsHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetPriceListsDto>> Handle(
            GetPriceListsQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetPriceListsDto>> GetDtoAsync(
            GetPriceListsQuery request,
            CancellationToken cancellationToken)
        {
            var priceLists = await _context
               .Set<PriceList>()
               .ProjectTo<GetPriceListsDto>(_provider)
               .ToListAsync(cancellationToken);

            return priceLists;
        }
    }
}
