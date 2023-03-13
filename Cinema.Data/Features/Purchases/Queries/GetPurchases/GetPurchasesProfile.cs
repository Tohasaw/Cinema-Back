using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchases
{
    internal class GetPurchasesProfile : Profile
    {
        public GetPurchasesProfile()
        {
            CreateMap<Purchase, GetPurchasesDto>();
        }
    }
}
