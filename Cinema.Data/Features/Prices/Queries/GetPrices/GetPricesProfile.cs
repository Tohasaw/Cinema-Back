using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Prices.Queries.GetPrices
{
    internal sealed class GetPricesProfile : Profile
    {
        public GetPricesProfile()
        {
            CreateMap<Price, GetPricesDto>();
        }
    }
}
