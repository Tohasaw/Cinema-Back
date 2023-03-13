using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Movies.Queries.GetMovies
{
    public sealed class GetMoviesProfile : Profile
    {
        public GetMoviesProfile()
        {
            CreateMap<Movie, GetMoviesDto>();
        }
    }
}
