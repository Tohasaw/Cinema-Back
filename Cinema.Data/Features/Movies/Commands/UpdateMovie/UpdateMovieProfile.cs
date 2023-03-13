using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Movies.Commands.UpdateMovie;

internal sealed class UpdateMovieProfile : Profile
{
    public UpdateMovieProfile()
    {
        CreateMap<UpdateMovieDto, Movie>();
    }
}