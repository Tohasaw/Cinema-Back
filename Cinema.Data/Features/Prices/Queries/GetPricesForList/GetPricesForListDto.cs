using Cinema.Data.Features.Seats.Queries.GetSeat;

namespace Cinema.Data.Features.Prices.Queries.GetPricesForList
{
    public sealed record GetPricesForListDto
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string HexColor { get; set; }
        public IEnumerable<GetRelationDto> Relation { get; set; }
    }
    public sealed record GetRelationDto
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }
        public int PriceId { get; set; }
    }
}
