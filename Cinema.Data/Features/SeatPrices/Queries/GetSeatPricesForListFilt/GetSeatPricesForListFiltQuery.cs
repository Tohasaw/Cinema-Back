using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using Cinema.Data.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForListFilt
{
    public sealed record GetSeatPricesForListFiltQuery(int TableEntryId) : IRequest<IEnumerable<GetSeatPricesForListFiltDto>>;
    internal sealed class GetSeatPricesForListFiltHandler : IRequestHandler<GetSeatPricesForListFiltQuery, IEnumerable<GetSeatPricesForListFiltDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        private readonly IMapper _mapper;
        public GetSeatPricesForListFiltHandler(
            AppDbContext context,
            IConfigurationProvider provider,
            IMapper mapper)
        {
            _context = context;
            _provider = provider;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetSeatPricesForListFiltDto>> Handle(
            GetSeatPricesForListFiltQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request.TableEntryId, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetSeatPricesForListFiltDto>> GetDtoAsync(
            int tableEntryId,
            CancellationToken cancellationToken)
        {
            var tableEntry = await _context
                .Set<TableEntry>()
                .SingleOrDefaultAsync(t => t.Id == tableEntryId);
            if (tableEntry == null) throw new BadRequestException("Выбранной позиции расписания не существует.");
            var tickets = await _context
                .Set<Ticket>()
                .Include(p => p.Purchase)
                .Where(t => t.Purchase.TableEntryId == tableEntryId && !t.Cancelled)
                .ToListAsync(cancellationToken);

            var seatPrices = await _context
               .Set<SeatPrice>()
               .Include(s => s.Price)
               .Include(s => s.Seat)
               .Where(s => s.PriceListId == tableEntry.PriceListId && s.Seat.IsAvailable == true)
               //.ProjectTo<GetSeatPricesForListFiltDto>(_provider)
               .ToListAsync(cancellationToken);

            var finSeatPrices = new List<SeatPrice>();
            foreach (var seatPrice in seatPrices)
            {
                if (!tickets.Any(t => t.SeatId == seatPrice.SeatId)) finSeatPrices.Add(seatPrice);
            }
            var dto = new List<GetSeatPricesForListFiltDto>();
            _mapper.Map(finSeatPrices, dto);
            return dto;
        }
    }
}
