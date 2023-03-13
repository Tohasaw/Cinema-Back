using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.SeatPrices.Commands.CreateSeatPrices;

public sealed record CreateSeatPricesDto
{
    public int SeatId { get; set; }
    public int PriceId { get; set; }
    public int PriceListId { get; set; }
}
