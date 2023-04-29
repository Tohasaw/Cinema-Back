using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchasesCountQuery
{
    public sealed record GetPurchasesCountQuery() : IRequest<int>;

    public sealed class GetPurchasesCountHandler : IRequestHandler<GetPurchasesCountQuery, int>
    {
        private readonly DbContext _context;

        public GetPurchasesCountHandler(
            DbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
        }

        public async Task<int> Handle(
            GetPurchasesCountQuery request,
            CancellationToken cancellationToken)
        {
            return await GetDtoAsync(cancellationToken);
        }

        private async Task<int> GetDtoAsync(
            CancellationToken cancellationToken)
        {
            return await _context
                .Set<Purchase>()
                .CountAsync(cancellationToken);
        }
    }
}
