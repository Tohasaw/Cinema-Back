using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchaseByKey
{
    public sealed record GetPurchaseByKeyQuery(string RefundKey) : IRequest<GetPurchaseByKeyDto>;

    internal sealed class GetPurchaseByKeyHandler : IRequestHandler<GetPurchaseByKeyQuery, GetPurchaseByKeyDto>
    {
        private readonly DbContext _context;
        private readonly IConfigurationProvider _provider;

        public GetPurchaseByKeyHandler(
            DbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }

        public async Task<GetPurchaseByKeyDto> Handle(
            GetPurchaseByKeyQuery request,
            CancellationToken cancellationToken)
        {
            var exists = await IsExistsPurchaseAsync(
                request.RefundKey,
                cancellationToken);
            if (!exists)
                throw new NotFoundException("Покупка не найдена");

            var dto = await GetDtoAsync(
                request.RefundKey,
                cancellationToken);

            return dto;
        }

        private async Task<bool> IsExistsPurchaseAsync(
        string refundKey,
        CancellationToken cancellationToken)
        {
            var exists = await _context
                .Set<Purchase>()
                .AnyAsync(p => p.RefundKey == refundKey, cancellationToken);

            return exists;
        }

        private async Task<GetPurchaseByKeyDto> GetDtoAsync(
            string refundKey,
            CancellationToken cancellationToken)
        {
            return await _context
                .Set<Purchase>()
                .Where(p => p.RefundKey == refundKey)
                .Include(p => p.TableEntry)
                .Include(p => p.TableEntry.Movie)
                .Include(p => p.Tickets)
                //.ThenInclude(p => p.SeatInfo)
                //.ThenInclude(p => p.Seat)
                .ProjectTo<GetPurchaseByKeyDto>(_provider)
                .SingleAsync(cancellationToken);
        }
    }
}
