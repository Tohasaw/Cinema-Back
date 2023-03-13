using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.Seats.Queries.GetSeats
{
    public sealed record GetSeatsDto
    {
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
