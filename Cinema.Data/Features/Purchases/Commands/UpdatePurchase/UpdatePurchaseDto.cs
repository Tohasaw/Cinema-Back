namespace Cinema.Data.Features.Purchases.Commands.UpdatePurchase
{
    public sealed record UpdatePurchaseDto
    {
        public int TableEntryId { get; set; }
        public DateTime DateTime { get; set; }
        public string? EmailAddress { get; set; }
        public bool AdvertAccepted { get; set; }
        public string PhoneNumber { get; set; }

        public string RefundKey { get; set; }
        public double PriceTotal { get; set; }
    }
}
