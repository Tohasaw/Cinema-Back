using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Users.Commands.CreateUser;

internal sealed class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}