using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPrice
{
    internal sealed class GetSeatPriceProfile : Profile
    {
        public GetSeatPriceProfile()
        {
            CreateMap<SeatPrice, GetSeatPriceDto>();
        }
    }
}
