using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.PriceLists.Queries.GetPriceLists
{
    public sealed record GetPriceListsDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
    }
}
