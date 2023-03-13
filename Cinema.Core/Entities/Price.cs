using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class Price
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string HexColor { get; set; }
        public ICollection<PriceListRelation> PriceListRelations { get; set; } = new List<PriceListRelation>();
        public ICollection<SeatPrice> SeatPrices { get; set; } = new List<SeatPrice>();
    }
}

