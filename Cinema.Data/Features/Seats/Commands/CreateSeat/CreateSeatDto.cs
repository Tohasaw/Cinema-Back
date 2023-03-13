using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Seats.Commands.CreateSeat
{
    public sealed record CreateSeatDto
    {
        public bool IsAvailable { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
