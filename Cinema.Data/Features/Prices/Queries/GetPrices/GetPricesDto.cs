using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.Prices.Queries.GetPrices
{
    public sealed record GetPricesDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string HexColor { get; set; }
    }
}
