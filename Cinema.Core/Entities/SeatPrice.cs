using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class SeatPrice
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int PriceId { get; set; }
        public int PriceListId { get; set; }
        public Seat Seat { get; set; }
        public Price Price { get; set; }
        public PriceList PriceList { get; set; }
    }
}
