using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForListFilt
{
    internal sealed class GetSeatPricesForListFiltProfile : Profile
    {
        public GetSeatPricesForListFiltProfile()
        {
            CreateMap<SeatPrice, GetSeatPricesForListFiltDto>();
            CreateMap<Price, GetPriceFilt>();
            CreateMap<Seat, GetSeatFilt>();
        }
    }
}
