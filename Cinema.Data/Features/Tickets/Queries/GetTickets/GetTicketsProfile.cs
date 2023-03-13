using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Tickets.Queries.GetTickets
{
    internal sealed class GetTicketsProfile : Profile
    {
        public GetTicketsProfile()
        {
            CreateMap<Ticket, GetTicketsDto>();
        }
    }
}
