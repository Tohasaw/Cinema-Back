using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Prices.Queries.GetPrices
{
    public sealed record GetPricesQuery() : IRequest<IEnumerable<GetPricesDto>>;
    internal sealed class GetPricesHandler : IRequestHandler<GetPricesQuery, IEnumerable<GetPricesDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetPricesHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetPricesDto>> Handle(
            GetPricesQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetPricesDto>> GetDtoAsync(
            CancellationToken cancellationToken)
        {
            var prices = await _context
               .Set<Price>()
               .ProjectTo<GetPricesDto>(_provider)
               .ToListAsync(cancellationToken);

            return prices;
        }
    }
}
