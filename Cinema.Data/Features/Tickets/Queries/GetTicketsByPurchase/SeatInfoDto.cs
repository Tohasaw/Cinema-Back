using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.Tickets.Queries.GetTicketsByPurchase
{
    public sealed record SeatInfoDto
    {
        public string Time { get; set; }
        public double Price { get; set; }
        public GetSeatDto Seat { get; set; }
    }
}
