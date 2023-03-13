using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Prices.Commands.CreatePrice
{
    public sealed record CreatePriceDto
    {
        public double Value { get; set; }
        public string HexColor { get; set; }
    }
}
