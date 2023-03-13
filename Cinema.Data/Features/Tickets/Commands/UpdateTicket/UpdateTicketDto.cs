namespace Cinema.Data.Features.Tickets.Commands.UpdateTicket
{
    public sealed record UpdateTicketDto
    {
        public int Id { get; set; }
        public int SeatId { get; set; }
        public int PurchaseId { get; set; }
        public bool Cancelled { get; set; }
        public bool Visited { get; set; }
        public string Key { get; set; }
        public double Price { get; set; }
    }
}
