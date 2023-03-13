using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Purchases.Commands.UpdatePurchase;
internal sealed class UpdatePurchaseProfile : Profile
{
    public UpdatePurchaseProfile()
    {
        CreateMap<UpdatePurchaseDto, Purchase>();
    }
}

