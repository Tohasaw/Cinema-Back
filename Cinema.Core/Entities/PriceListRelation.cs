using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Entities
{
    public sealed class PriceListRelation
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }
        public int PriceId { get; set; }
        public PriceList PriceList { get; set; }
        public Price Price { get; set; }
    }
}
