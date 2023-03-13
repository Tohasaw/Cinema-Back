using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.TableEntries.Commands.CreateTableEntry;

public sealed record CreateTableEntryDto
{
    public DateTime DateTime { get; set; }
    public int PriceListId { get; set; }
    public int MovieId { get; set; }
}
