using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPrice
{
    public sealed record GetSeatPriceQuery(int SeatPriceId) : IRequest<GetSeatPriceDto>;
    internal sealed class GetSeatPriceHandler : IRequestHandler<GetSeatPriceQuery, GetSeatPriceDto>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetSeatPriceHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<GetSeatPriceDto> Handle(
            GetSeatPriceQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<GetSeatPriceDto> GetDtoAsync(
            GetSeatPriceQuery request,
            CancellationToken cancellationToken)
        {
            var seatPrices = await _context
               .Set<SeatPrice>()
               .Where(x => x.Id == request.SeatPriceId)
               .Include(x => x.Seat)
               .Include(x => x.Price)
               .ProjectTo<GetSeatPriceDto>(_provider)
               .FirstOrDefaultAsync(cancellationToken);

            return seatPrices;
        }
    }
}
