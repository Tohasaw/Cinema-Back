namespace Cinema.Data.Features.Tickets.Commands.CancelTickets
{
    public sealed record CancelTicketsDto
    {
        public string RefundKey { get; set; }
        public int[] TicketIds { get; set; }
    }
}
