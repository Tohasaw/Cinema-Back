using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Cinema.Core.Entities;

namespace Cinema.Data.Services.JwtTokenServices;

/// <summary>
/// Сервис создания jwt токенов
/// </summary>
public sealed class JwtSecurityTokenService
{
    /// <summary>
    /// конфигурация приложения
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор сервиса создания jwt токенов
    /// </summary>
    /// <param name="configuration">Конфигурация приложения</param>
    public JwtSecurityTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Метод создания jwt токена
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <returns>Токен в виде строки</returns>
    public string Create(User user)
    {
        var claims = CreateClaims(user);

        var securityKey = GetSecurityKey();

        var signingCredentials = GetSigningCredentials(securityKey);

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Authentications:Issuer"],
            _configuration["Authentications:Audience"],
            claims,
            DateTime.Now,
            DateTime.Now.AddHours(1),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        ;
    }

    private static SigningCredentials GetSigningCredentials(SecurityKey securityKey)
    {
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        return signingCredentials;
    }

    private SymmetricSecurityKey GetSecurityKey()
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentications:Security"]));

        return symmetricSecurityKey;
    }

    private static IEnumerable<Claim> CreateClaims(User user)
    {
        var claims = new Claim[]
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, user.Role.Name),
            new("Password", user.Password!),
        };

        return claims;
    }
}