using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchases
{
    public sealed record GetPurchasesQuery() : IRequest<IEnumerable<GetPurchasesDto>>;

    internal sealed class GetPurchasesHandler : IRequestHandler<GetPurchasesQuery, IEnumerable<GetPurchasesDto>>
    {
        private readonly DbContext _context;
        private readonly IConfigurationProvider _provider;

        public GetPurchasesHandler(
            DbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task<IEnumerable<GetPurchasesDto>> Handle(
            GetPurchasesQuery request,
            CancellationToken cancellationToken)
        {
            return await GetDtoAsync(request, cancellationToken);
        }

        private async Task<IEnumerable<GetPurchasesDto>> GetDtoAsync(
            GetPurchasesQuery request,
            CancellationToken cancellationToken)
        {
            return await _context
                .Set<Purchase>()
                .OrderByDescending(x => x.DateTime)
                .ProjectTo<GetPurchasesDto>(_provider)
                .ToListAsync(cancellationToken);
        }
    }
}
