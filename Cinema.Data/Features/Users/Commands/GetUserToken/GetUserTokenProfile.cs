using AutoMapper;
using Cinema.Core.Entities;

namespace Cinema.Data.Features.Users.Commands.GetUserToken;

internal sealed class GetUserTokenProfile : Profile
{
    public GetUserTokenProfile()
    {
        CreateMap<GetUserTokenDto, User>();
    }
}