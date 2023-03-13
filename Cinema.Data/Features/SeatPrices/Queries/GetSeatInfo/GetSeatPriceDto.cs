using Cinema.Data.Features.Prices.Queries.GetPrices;
using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPrice
{
    public sealed record GetSeatPriceDto
    {
        public int Id { get; set; }
        public int PriceId { get; set; }
        public int PriceListId { get; set; }
        public int SeatId { get; set; }
        public GetSeatDto Seat { get; set; }
    }
}
