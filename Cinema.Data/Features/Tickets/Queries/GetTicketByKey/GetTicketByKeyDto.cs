using Cinema.Data.Features.Purchases.Queries.GetPurchase;
using Cinema.Data.Features.Seats.Queries.GetSeat;
using Cinema.Data.Features.TableEntries.Queries.GetTableEntry;

namespace Cinema.Data.Features.Tickets.Queries.GetTicketByKey
{
    public sealed record GetTicketByKeyDto
    {
        public int Id { get; set; }
        public bool Cancelled { get; set; }
        public bool Visited { get; set; }
        public double Price { get; set; }
        public GetPurchaseDto Purchase { get; set; }
        public GetSeatDto Seat { get; set; }
    }
}
