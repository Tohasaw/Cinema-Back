using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.PriceListRelations.Commands.CreatePriceListRelation
{
    internal sealed class CreatePriceListRelationProfile : Profile
    {
        public CreatePriceListRelationProfile()
        {
            CreateMap<CreatePriceListRelationDto, PriceListRelation>();
        }
    }
}
