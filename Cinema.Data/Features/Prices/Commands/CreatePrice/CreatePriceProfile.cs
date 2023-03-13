using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Prices.Commands.CreatePrice
{
    internal sealed class CreatePriceProfile : Profile
    {
        public CreatePriceProfile()
        {
            CreateMap<CreatePriceDto, Price>();
        }
    }
}
