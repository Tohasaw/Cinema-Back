using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfosFilt
{
    public sealed record GetSeatInfosFiltDto
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }
        public int SeatId { get; set; }
        public GetSeatDto Seat { get; set; }
        public bool Reserved { get; set; }
    }
}
