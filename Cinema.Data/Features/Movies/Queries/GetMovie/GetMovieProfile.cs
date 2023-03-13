using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Movies.Queries.GetMovie
{
    public sealed class GetMovieProfile : Profile
    {
        public GetMovieProfile()
        {
            CreateMap<Movie, GetMovieDto>();
        }
    }
}
