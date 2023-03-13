using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.SeatInfos.Queries.GetSeatInfosFilt
{
    public sealed record TableEntryDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
    }
}
