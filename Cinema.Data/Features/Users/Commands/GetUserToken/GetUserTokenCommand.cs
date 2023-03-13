using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cinema.Core.Entities;
using Cinema.Data.Exceptions;
using Cinema.Data.Services.JwtTokenServices;
using Cinema.Data.Services.Users;

namespace Cinema.Data.Features.Users.Commands.GetUserToken;

public sealed record GetUserTokenCommand(GetUserTokenDto Dto) : IRequest<object>;

internal sealed class GetUserTokenHandler : IRequestHandler<GetUserTokenCommand, object>
{
    private readonly DbContext _context;
    private readonly JwtSecurityTokenService _jwtSecurityTokenService;

    public GetUserTokenHandler(
        DbContext context,
        CurrentUserService currentUserService,
        JwtSecurityTokenService jwtSecurityTokenService,
        IMapper mapper)
    {
        _context = context;
        _jwtSecurityTokenService = jwtSecurityTokenService;
    }

    public async Task<object> Handle(
        GetUserTokenCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsUserAsync(
            request.Dto.Email,
            cancellationToken);
        if (!exists)
            throw new BadRequestException("Неверные данные");

        var user = await GetUserAsync(request.Dto.Email, cancellationToken);

        var access = user.Password == request.Dto.Password;
        if (!access)
            throw new BadRequestException("Неверные данные");

        var token = _jwtSecurityTokenService.Create(user);
        var response = new
        {
            token = token,
            userRole = user.Role.Name,
            email = request.Dto.Email,
        };
        return response;
    }

    private async Task<bool> IsExistsUserAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<User>()
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    private async Task<User> GetUserAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<User>()
            .Include(u => u.Role)
            .AsNoTracking()
            .SingleAsync(u => u.Email == email, cancellationToken);
    }
}