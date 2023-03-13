using Cinema.Data.Features.Movies.Queries.GetMovie;

namespace Cinema.Data.Features.Movies.Queries.GetMoviesWithEntries
{
    public sealed record GetMoviesWithEntriesDto
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Genres { get; set; }
        public string AgeLimit { get; set; }
        public string Image { get; set; }
        public IEnumerable<GetTableEntryDto> TableEntries { get; set; }
    }
    public sealed record GetTableEntryDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int PriceListId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
