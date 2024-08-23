using MediatR;
using Zeeget.Gateway.API.Modules.Authentication.Requests;
using Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak;
using Zeeget.Shared.Commons.Api.CustomResponse;

namespace Zeeget.Gateway.API.Modules.Authentication.Handlers
{
    public class LoginUserHandler(IKeycloakService keycloakService) : IRequestHandler<LoginUserCommand, Response<Guid>>
    {
        private readonly IKeycloakService _keycloakService = keycloakService;

        public async Task<Response<Guid>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _keycloakService.LoginUserAsync(request.User);

            return new Response<Guid>();
        }
    }
}
