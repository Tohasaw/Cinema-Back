using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.PriceLists.Queries.GetPriceLists
{
    internal sealed class GetPriceListsProfile : Profile
    {
        public GetPriceListsProfile()
        {
            CreateMap<PriceList, GetPriceListsDto>();
        }
    }
}
