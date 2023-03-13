using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Seats.Queries.GetSeats
{
    public sealed record GetSeatsQuery() : IRequest<IEnumerable<GetSeatsDto>>;
    internal sealed class GetSeatsHandler : IRequestHandler<GetSeatsQuery, IEnumerable<GetSeatsDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetSeatsHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetSeatsDto>> Handle(
            GetSeatsQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetSeatsDto>> GetDtoAsync(
            GetSeatsQuery request,
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<Seat>()
               .ProjectTo<GetSeatsDto>(_provider)
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
