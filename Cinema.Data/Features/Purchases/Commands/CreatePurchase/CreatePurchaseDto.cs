using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.SeatPrices.Commands.CreatePurchase;

public sealed record CreatePurchaseDto
{
    public int Id { get; set; }
    public int TableEntryId { get; set; }
    public string PhoneNumber { get; set; }
}
