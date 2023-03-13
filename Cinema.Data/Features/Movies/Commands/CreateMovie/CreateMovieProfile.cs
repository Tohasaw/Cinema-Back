using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Movies.Commands.CreateMovie
{
    internal sealed class CreateMovieProfile : Profile
    {
        public CreateMovieProfile()
        {
            CreateMap<CreateMovieDto, Movie>();
        }
    }
}
