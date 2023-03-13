using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class Seat
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<SeatPrice> SeatPrices { get; set; } = new List<SeatPrice>();
    }
}
