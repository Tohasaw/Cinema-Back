using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatPrices.Commands.CreateSeatPrices
{
    internal sealed class CreateSeatPricesProfile : Profile
    {
        public CreateSeatPricesProfile()
        {
            CreateMap<CreateSeatPricesDto, SeatPrice>();
        }
    }
}
