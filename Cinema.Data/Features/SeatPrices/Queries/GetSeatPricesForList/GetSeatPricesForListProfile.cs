using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForList
{
    internal sealed class GetSeatPricesForListProfile : Profile
    {
        public GetSeatPricesForListProfile()
        {
            CreateMap<SeatPrice, GetSeatPricesForListDto>();
            CreateMap<Price, GetPrice>();
        }
    }
}
