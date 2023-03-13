using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class Purchase
    {
        public int Id { get; set; }
        public int TableEntryId { get; set; }
        public DateTime DateTime { get; set; }
        public string? EmailAddress { get; set; }
        public bool AdvertAccepted { get; set; }
        public string PhoneNumber { get; set; }
        public string RefundKey { get; set; }
        public double? PriceTotal { get; set; }
        public TableEntry TableEntry { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
