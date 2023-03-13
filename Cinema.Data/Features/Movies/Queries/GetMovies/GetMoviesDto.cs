using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Features.Movies.Queries.GetMovies
{
    public sealed record GetMoviesDto
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
    }
}
