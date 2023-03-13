using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchase
{
    public sealed record GetPurchaseQuery : IRequest<GetPurchaseDto>
    {
        public GetPurchaseQuery(int purchaseId)
        {
            PurchaseId = purchaseId;
        }

        public int PurchaseId { get; }
    }

    internal sealed class GetPurchaseHandler : IRequestHandler<GetPurchaseQuery, GetPurchaseDto>
    {
        private readonly DbContext _context;
        private readonly IConfigurationProvider _provider;

        public GetPurchaseHandler(
            DbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task<GetPurchaseDto> Handle(
            GetPurchaseQuery request,
            CancellationToken cancellationToken)
        {
            var exists = await IsExistsPurchaseAsync(
                request.PurchaseId,
                cancellationToken);
            if (!exists)
                throw new NotFoundException("Покупка не найдена");

            var dto = await GetDtoAsync(
                request.PurchaseId,
                cancellationToken);

            return dto;
        }

        private async Task<bool> IsExistsPurchaseAsync(
        int purchaseId,
        CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Purchase>()
                .AnyAsync(p => p.Id == purchaseId, cancellationToken);

            return exists;
        }

        private async Task<GetPurchaseDto> GetDtoAsync(
            int purchaseId,
            CancellationToken cancellationToken)
        {
            return await _context
                .Set<Purchase>()
                .Where(p => p.Id == purchaseId)
                .ProjectTo<GetPurchaseDto>(_provider)
                .SingleAsync(cancellationToken);
        }
    }
}
