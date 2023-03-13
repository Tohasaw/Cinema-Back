using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfos
{
    public sealed record GetSeatInfosDto
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public double Price { get; set; }
        public int SeatId { get; set; }
        public GetSeatDto Seat { get; set; }
    }
}
