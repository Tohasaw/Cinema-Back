using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Seats.Queries.GetSeats
{
    internal sealed class GetSeatsProfile : Profile
    {
        public GetSeatsProfile()
        {
            CreateMap<Seat, GetSeatsDto>();
        }
    }
}
