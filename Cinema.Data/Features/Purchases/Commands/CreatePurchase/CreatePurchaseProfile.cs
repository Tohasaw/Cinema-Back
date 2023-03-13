using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatPrices.Commands.CreatePurchase
{
    internal sealed class CreatePurchaseProfile : Profile
    {
        public CreatePurchaseProfile()
        {
            CreateMap<CreatePurchaseDto, Purchase>();
        }
    }
}
