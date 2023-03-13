using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Tickets.Queries.GetTickets
{
    public sealed record GetTicketsQuery() : IRequest<IEnumerable<GetTicketsDto>>;
    internal sealed class GetTableEntriesMovieHandler : IRequestHandler<GetTicketsQuery, IEnumerable<GetTicketsDto>>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetTableEntriesMovieHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<IEnumerable<GetTicketsDto>> Handle(
            GetTicketsQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<IEnumerable<GetTicketsDto>> GetDtoAsync(
            GetTicketsQuery request,
            CancellationToken cancellationToken)
        {
            var seatinfos = await _context
               .Set<Ticket>()
               .ProjectTo<GetTicketsDto>(_provider)
               .ToListAsync(cancellationToken);

            return seatinfos;
        }
    }
}
