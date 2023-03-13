using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Tickets.Queries.GetTicket
{
    internal sealed class GetTicketProfile : Profile
    {
        public GetTicketProfile()
        {
            CreateMap<Ticket, GetTicketDto>();
        }
    }
}
