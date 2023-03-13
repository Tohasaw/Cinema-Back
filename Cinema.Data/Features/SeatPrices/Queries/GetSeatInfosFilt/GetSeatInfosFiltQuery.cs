using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfosFilt
{
    public sealed record GetSeatInfosFiltQuery(TableEntryDto dto) : IRequest<IEnumerable<GetSeatInfosFiltDto>>;
    internal sealed class GetSeatInfosFiltHandler : IRequestHandler<GetSeatInfosFiltQuery, IEnumerable<GetSeatInfosFiltDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetSeatInfosFiltHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetSeatInfosFiltDto>> Handle(
            GetSeatInfosFiltQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetSeatInfosFiltDto>> GetDtoAsync(
            GetSeatInfosFiltQuery request,
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<SeatPrice>()
               //.Where(s => s.Time.ToTimeSpan() == request.dto.DateTime.TimeOfDay)
               .Include(info => info.Seat)
               .ProjectTo<GetSeatInfosFiltDto>(_provider)
               .ToListAsync(cancellationToken);

            var tickets = await _context
                .Set<Ticket>()
                .Include(p => p.Purchase)
                //.Where(t => t.Purchase.TableEntryId == request.dto.Id && !t.IsCancelled)
                .ToListAsync(cancellationToken);

            foreach (var ticket in tickets)
            {
                seatinfos
                    .FindAll(s => s.Id == ticket.SeatId)
                    .ForEach(s => s.Reserved = true);
            }

            return seatinfos;
        }
    }
}
