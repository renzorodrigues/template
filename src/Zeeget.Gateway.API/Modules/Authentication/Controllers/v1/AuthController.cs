using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Shared.Api;

namespace Zeeget.Gateway.API.Modules.Authentication.Controllers.v1
{
    public class AuthController(ILogger<ApiController> logger, IMediator mediator)
        : ApiController(logger, mediator)
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] UserRegistrationDto userRegistrationDto,
            CancellationToken cancellationToken
        ) =>
            await ExecuteCommandAsync(
                new RegisterUserCommand(userRegistrationDto),
                cancellationToken
            );

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] UserLoginDto userLoginDto,
            CancellationToken cancellationToken
        ) => await ExecuteQueryAsync(new LoginUserQuery(userLoginDto), cancellationToken);
    }
}
