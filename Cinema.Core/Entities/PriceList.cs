using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class PriceList
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public ICollection<TableEntry> TableEntries { get; set; } = new List<TableEntry>();
        public ICollection<PriceListRelation> PriceListRelations { get; set; } = new List<PriceListRelation>();
        public ICollection<SeatPrice> SeatPrices { get; set; } = new List<SeatPrice>();
    }
}
