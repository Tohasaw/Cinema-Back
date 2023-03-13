using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.PriceLists.Commands.CreatePriceList
{
    internal sealed class CreatePriceListProfile : Profile
    {
        public CreatePriceListProfile()
        {
            CreateMap<CreatePriceListDto, PriceList>();
        }
    }
}
