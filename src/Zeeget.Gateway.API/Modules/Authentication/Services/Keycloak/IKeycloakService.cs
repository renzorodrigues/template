using Zeeget.Gateway.API.Modules.Authentication.Dtos;

namespace Zeeget.Gateway.API.Modules.Authentication.Services.Keycloak
{
    public interface IKeycloakService
    {
        Task<bool> RegisterUserAsync(UserRegistrationDto user);
        Task<string> LoginUserAsync(UserLoginDto user);
    }
}
