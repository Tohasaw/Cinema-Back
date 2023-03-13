using Cinema.Data.Features.Movies.Queries.GetMovie;

namespace Cinema.Data.Features.Movies.Queries.GetMovieWithEntries
{
    public sealed record GetMovieWithEntriesDto
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Countries { get; set; }
        public string Genres { get; set; }
        public string Director { get; set; }
        public int Length { get; set; }
        public string AgeLimit { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string VideoLink { get; set; }
        public IEnumerable<GetTableEntryDto> TableEntries { get; set; }
    }
    public sealed record GetTableEntryDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
    }
}
