using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class Ticket
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int PurchaseId { get; set; }
        public bool Cancelled { get; set; }
        public bool Visited { get; set; }
        public string Key { get; set; }
        public double Price { get; set; }
        public Purchase Purchase { get; set; }
        public Seat Seat { get; set; }
    }
}
