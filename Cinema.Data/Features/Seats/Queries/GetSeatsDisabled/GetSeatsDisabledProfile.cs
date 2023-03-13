using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Seats.Queries.GetSeatsDisabled
{
    internal sealed class GetSeatsDisabledProfile : Profile
    {
        public GetSeatsDisabledProfile()
        {
            CreateMap<Seat, GetSeatsDisabledDto>();
        }
    }
}
