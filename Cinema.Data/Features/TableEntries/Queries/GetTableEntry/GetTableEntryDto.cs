using Cinema.Data.Features.Movies.Queries.GetMovie;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntry
{
    public sealed record GetTableEntryDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
        public int PriceListId { get; set; }
        public GetMovieDto Movie { get; set; }
    }
}
