using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.PriceLists.Commands.CreatePriceList
{
    public sealed record CreatePriceListDto
    {
        public string Name { get; set; }
    }
}
