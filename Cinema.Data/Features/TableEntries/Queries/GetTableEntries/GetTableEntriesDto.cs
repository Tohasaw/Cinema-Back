namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntries
{
    internal sealed record GetTableEntriesDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
        public int PriceListId { get; set; }
    }
}
