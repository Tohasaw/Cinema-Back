using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfos
{
    public sealed record GetSeatInfosQuery() : IRequest<IEnumerable<GetSeatInfosDto>>;
    internal sealed class GetSeatInfosHandler : IRequestHandler<GetSeatInfosQuery, IEnumerable<GetSeatInfosDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetSeatInfosHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetSeatInfosDto>> Handle(
            GetSeatInfosQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetSeatInfosDto>> GetDtoAsync(
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<SeatPrice>()
               .ProjectTo<GetSeatInfosDto>(_provider)
               .Include(x => x.Seat)
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
