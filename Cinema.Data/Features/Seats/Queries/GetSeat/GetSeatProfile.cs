using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Seats.Queries.GetSeat
{
    internal class GetSeatProfile : Profile
    {
        public GetSeatProfile()
        {
            CreateMap<Seat, GetSeatDto>();
        }
    }
}
