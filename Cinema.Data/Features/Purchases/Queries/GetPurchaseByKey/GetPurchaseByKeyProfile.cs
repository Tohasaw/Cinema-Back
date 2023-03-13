using AutoMapper;
using Cinema.Core.Entities;
using Cinema.Data.Features.Tickets.Queries.GetTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchaseByKey
{
    internal class GetPurchaseByKeyProfile : Profile
    {
        public GetPurchaseByKeyProfile()
        {
            CreateMap<Purchase, GetPurchaseByKeyDto>();
            CreateMap<Ticket, PurchaseTicketDto>();
            CreateMap<Seat, PurchaseSeatDto>();
            CreateMap<Movie, PurchaseMovieDto>();
            CreateMap<TableEntry, PurchaseTableEntryDto>();
        }
    }
}
