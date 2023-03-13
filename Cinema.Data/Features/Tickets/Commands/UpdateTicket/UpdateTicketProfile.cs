using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Tickets.Commands.UpdateTicket;
internal sealed class UpdateTicketProfile : Profile
{
    public UpdateTicketProfile()
    {
        CreateMap<UpdateTicketDto, Ticket>();
    }
}

