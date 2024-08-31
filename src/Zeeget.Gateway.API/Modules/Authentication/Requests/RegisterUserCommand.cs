using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Api;
using Zeeget.Shared.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record RegisterUserCommand : ICommand<Result>
    {
        public UserRegistrationDto User { get; set; }

        public RegisterUserCommand(UserRegistrationDto user)
        {
            User = user;
        }
    }
}
