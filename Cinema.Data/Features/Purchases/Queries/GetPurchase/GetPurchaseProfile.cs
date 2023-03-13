using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchase
{
    internal class GetPurchaseProfile : Profile
    {
        public GetPurchaseProfile()
        {
            CreateMap<Purchase, GetPurchaseDto>();
        }
    }
}
