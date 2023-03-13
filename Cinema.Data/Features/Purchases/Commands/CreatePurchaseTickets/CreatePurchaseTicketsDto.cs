using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Purchases.Commands.CreatePurchaseTickets;

public sealed record CreatePurchaseTicketsDto
{
    public int TableEntryId { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public bool AdvertAccepted { get; set; }
    public IEnumerable<int> SeatIds { get; set; }
}
