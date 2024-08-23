using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record RegisterUserCommand : ICommand<Guid>
    {
        public UserRegistrationDto User { get; set; }

        public RegisterUserCommand(UserRegistrationDto user)
        {
            User = user;
        }
    }
}
