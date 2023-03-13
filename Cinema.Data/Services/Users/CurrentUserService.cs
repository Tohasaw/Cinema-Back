using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cinema.Data.Services.Users;

public sealed class CurrentUserService
{
    private readonly HttpContextAccessor _context;

    public CurrentUserService(HttpContextAccessor context)
    {
        _context = context;
    }

    public int Id => int.Parse(_context
        .HttpContext!
        .User
        .Claims
        .FirstOrDefault(c => c.Type == "Id")!
        .Value);

    public string? Login => _context
        .HttpContext!
        .User
        .Claims
        .FirstOrDefault(c => c.Type == ClaimTypes.Email)!
        .Value;
}