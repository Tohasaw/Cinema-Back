namespace Cinema.Data.Features.Users.Commands.CreateUser;

public sealed record CreateUserDto(
    string Email,
    string Password);