using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Tickets.Queries.GetTicketByKey
{
    internal sealed class GetTicketByKeyProfile : Profile
    {
        public GetTicketByKeyProfile()
        {
            CreateMap<Ticket, GetTicketByKeyDto>();
        }
    }
}
