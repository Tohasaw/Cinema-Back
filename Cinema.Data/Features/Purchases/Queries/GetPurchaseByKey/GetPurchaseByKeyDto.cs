namespace Cinema.Data.Features.Purchases.Queries.GetPurchaseByKey
{
    public sealed record GetPurchaseByKeyDto
    {
        public int Id { get; set; }
        public int TableEntryId { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public double PriceTotal { get; set; }
        public PurchaseTableEntryDto TableEntry { get; set; }
        public IEnumerable<PurchaseTicketDto> Tickets { get; set;}

    }
    public sealed record PurchaseTicketDto
    {
        public int Id { get; set; }
        public bool Cancelled { get; set; }
        public double Price { get; set; }
        public string Key { get; set; }
        public PurchaseSeatDto Seat { get; set; }
    }
    public sealed record PurchaseSeatDto
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
    public sealed record PurchaseTableEntryDto
    {
        public DateTime DateTime { get; set; }
        public PurchaseMovieDto Movie { get; set; }
    }
    public sealed record PurchaseMovieDto
    {
        public string Title { get; set; }
    }
}
