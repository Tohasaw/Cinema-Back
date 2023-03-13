using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.PriceListRelations.Commands.CreatePriceListRelation
{
    public sealed record CreatePriceListRelationDto
    {
        public int PriceListId { get; set; }
        public int PriceId { get; set; }
    }
}
