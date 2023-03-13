using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Seats.Queries.GetSeatsDisabled
{
    public sealed record GetSeatsDisabledQuery() : IRequest<IEnumerable<GetSeatsDisabledDto>>;
    internal sealed class GetSeatsDisabledHandler : IRequestHandler<GetSeatsDisabledQuery, IEnumerable<GetSeatsDisabledDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetSeatsDisabledHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetSeatsDisabledDto>> Handle(
            GetSeatsDisabledQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetSeatsDisabledDto>> GetDtoAsync(
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<Seat>()
               .Where(s => s.IsAvailable == false)
               .ProjectTo<GetSeatsDisabledDto>(_provider)
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
