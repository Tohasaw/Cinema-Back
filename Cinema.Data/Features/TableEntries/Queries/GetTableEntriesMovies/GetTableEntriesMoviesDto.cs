using Cinema.Data.Features.Movies.Queries.GetMovie;
using Cinema.Data.Features.PriceLists.Queries.GetPriceLists;

namespace Cinema.Data.Features.TableEntries.Queries.GetTableEntriesMovies
{
    public sealed record GetTableEntriesMoviesDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieId { get; set; }
        public int PriceListId { get; set; }
        public GetPriceListsDto PriceList { get; set; }
        public GetMovieDto Movie { get; set; }
        public int Visit { get; set; }
        public string VisitPercent { get; set; }
    }
}
