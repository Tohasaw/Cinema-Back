using Cinema.Data.Features.TableEntries.Queries.GetTableEntry;

namespace Cinema.Data.Features.Purchases.Queries.GetPurchases
{
    public sealed record GetPurchasesDto
    {
        public int Id { get; set; }
        public int TableEntryId { get; set; }
        public DateTime DateTime { get; set; }
        public string EmailAddress { get; set; }
        public bool AdvertAccepted { get; set; }
        public string PhoneNumber { get; set; }
        public string RefundKey { get; set; }
        public double PriceTotal { get; set; }
        public GetTableEntryDto TableEntry { get; set; }
    }
}
