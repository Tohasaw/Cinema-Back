
using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.Tickets.Queries.GetTicket
{
    public sealed record GetTicketDto
    {
        public int Id { get; set; }
        public int PurchaseId { get; set; }
        public bool IsCancelled { get; set; }
        public double Price { get; set; }
        public GetSeatDto Seat { get; set; }
    }
}
