using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.PriceLists.Commands.UpdatePriceList;
internal sealed class UpdatePriceListProfile : Profile
{
    public UpdatePriceListProfile()
    {
        CreateMap<UpdatePriceListDto, PriceList>();
    }
}

