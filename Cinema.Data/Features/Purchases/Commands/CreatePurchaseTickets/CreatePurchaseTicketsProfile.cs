using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Purchases.Commands.CreatePurchaseTickets
{
    internal sealed class CreatePurchaseTicketsProfile : Profile
    {
        public CreatePurchaseTicketsProfile()
        {
            CreateMap<CreatePurchaseTicketsDto, Purchase>();
        }
    }
}
