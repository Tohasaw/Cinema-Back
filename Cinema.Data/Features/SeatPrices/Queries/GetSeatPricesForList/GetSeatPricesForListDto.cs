using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForList
{
    public sealed record GetSeatPricesForListDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int PriceListId { get; set; }
        public int PriceId { get; set; }
        public GetPrice Price { get; set; }
    }
    public sealed record GetPrice
    {
        public string HexColor { get; set; }
    }
}
