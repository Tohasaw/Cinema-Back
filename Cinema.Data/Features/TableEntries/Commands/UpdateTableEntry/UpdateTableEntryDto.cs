namespace Cinema.Data.Features.TableEntries.Commands.UpdateTableEntry
{
    public sealed record UpdateTableEntryDto
    {
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
        public int PriceListId { get; set; }
    }
}
