using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfosFilt
{
    internal sealed class GetSeatInfosFiltProfile : Profile
    {
        public GetSeatInfosFiltProfile()
        {
            CreateMap<SeatPrice, GetSeatInfosFiltDto>();
        }
    }
}
