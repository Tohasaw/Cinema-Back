using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Tickets.Commands.CreateTicket
{
    public sealed record CreateTicketDto
    {
        public int PurchaseId { get; set; }
        public int SeatId { get; set; }

    }
}
