using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Tickets.Queries.GetTicketsByPurchase
{
    internal sealed class GetTicketsByPurchaseProfile : Profile
    {
        public GetTicketsByPurchaseProfile()
        {
            CreateMap<Ticket, GetTicketsByPurchaseDto>();
            CreateMap<SeatPrice, SeatInfoDto>();
        }
    }
}
