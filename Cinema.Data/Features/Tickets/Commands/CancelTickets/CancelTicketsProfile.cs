using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Tickets.Commands.CancelTickets;
internal sealed class CancelTicketsProfile : Profile
{
    public CancelTicketsProfile()
    {
        CreateMap<CancelTicketsDto, Ticket>();
    }
}

