using AutoMapper;
using Cinema.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Seats.Commands.CreateSeat
{
    internal sealed class CreateSeatProfile : Profile
    {
        public CreateSeatProfile()
        {
            CreateMap<CreateSeatDto, Seat>();
        }
    }
}
