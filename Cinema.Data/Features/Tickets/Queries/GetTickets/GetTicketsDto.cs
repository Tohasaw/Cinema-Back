using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.Tickets.Queries.GetTickets
{
    public sealed record GetTicketsDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int PurchaseId { get; set; }
        public bool Cancelled { get; set; }
        public bool Visited { get; set; }
        public string Key { get; set; }
        public double Price { get; set; }
        public GetSeatDto Seat { get; set; }

    }
}
