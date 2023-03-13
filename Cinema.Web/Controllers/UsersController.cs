using Cinema.Data.Features.Users.Commands.CreateUser;
using Cinema.Data.Features.Users.Commands.GetUserToken;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Web.Controllers;
[Route("api")]
public class UsersController : Controller
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> GetUserToken(
        [FromBody] GetUserTokenDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetUserTokenCommand(dto), cancellationToken));
    }
    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreateUserCommand(dto), cancellationToken));
    }
}

