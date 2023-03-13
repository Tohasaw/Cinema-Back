using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Prices.Queries.GetPricesForList
{
    internal sealed class GetPricesForListProfile : Profile
    {
        public GetPricesForListProfile()
        {
            int priceListId = 0;
            CreateMap<Price, GetPricesForListDto>()
                .ForMember(dest => dest.Relation, src => src.MapFrom(src => src.PriceListRelations.Where(r => r.PriceListId == priceListId)));
            CreateMap<PriceListRelation, GetRelationDto>();
        }
    }
}
