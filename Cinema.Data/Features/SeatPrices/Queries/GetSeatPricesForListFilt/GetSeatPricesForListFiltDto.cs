using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForListFilt
{
    public sealed record GetSeatPricesForListFiltDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int PriceListId { get; set; }
        public int PriceId { get; set; }
        public GetPriceFilt Price { get; set; }
        public GetSeatFilt Seat { get; set; }
    }
    public sealed record GetPriceFilt
    {
        public string HexColor { get; set; }
        public double Value { get; set; }
    }
    public sealed record GetSeatFilt
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
