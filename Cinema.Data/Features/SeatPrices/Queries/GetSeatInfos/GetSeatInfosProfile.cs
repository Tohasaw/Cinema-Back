using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfos
{
    internal sealed class GetSeatInfosProfile : Profile
    {
        public GetSeatInfosProfile()
        {
            CreateMap<SeatPrice, GetSeatInfosDto>();
        }
    }
}
