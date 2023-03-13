using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class TableEntry
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int PriceListId { get; set; }
        public DateTime DateTime { get; set; }
        public Movie Movie { get; set; }
        public PriceList PriceList { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    }
}
