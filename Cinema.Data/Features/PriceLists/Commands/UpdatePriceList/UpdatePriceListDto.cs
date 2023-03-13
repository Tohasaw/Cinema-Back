namespace Cinema.Data.Features.PriceLists.Commands.UpdatePriceList
{
    public sealed record UpdatePriceListDto
    {
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
    }
}
