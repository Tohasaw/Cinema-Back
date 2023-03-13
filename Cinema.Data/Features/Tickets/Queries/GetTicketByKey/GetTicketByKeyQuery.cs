using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cinema.Core.Entities;
using Cinema.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data.Features.Tickets.Queries.GetTicketByKey
{
    public sealed record GetTicketByKeyQuery(string ticketKey) : IRequest<GetTicketByKeyDto>;
    internal sealed class GetTicketByKeyHandler : IRequestHandler<GetTicketByKeyQuery, GetTicketByKeyDto>
    {
        private readonly AppDbContext _context;
        private readonly IConfigurationProvider _provider;
        public GetTicketByKeyHandler(
            AppDbContext context,
            IConfigurationProvider provider)
        {
            _context = context;
            _provider = provider;
        }
        public async Task<GetTicketByKeyDto> Handle(
            GetTicketByKeyQuery request,
            CancellationToken cancellationToken)
        {
            var dto = await GetDtoAsync(request, cancellationToken);
            return dto;
        }
        private async Task<GetTicketByKeyDto> GetDtoAsync(
            GetTicketByKeyQuery request,
            CancellationToken cancellationToken)
        {
            var ticket = await _context
               .Set<Ticket>()
               .Where(x => x.Key == request.ticketKey)
               .ProjectTo<GetTicketByKeyDto>(_provider)
               .FirstOrDefaultAsync(cancellationToken);

            return ticket;
        }
    }
}
