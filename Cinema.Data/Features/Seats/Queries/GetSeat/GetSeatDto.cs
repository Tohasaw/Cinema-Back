using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Seats.Queries.GetSeat
{
    public sealed record GetSeatDto
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
