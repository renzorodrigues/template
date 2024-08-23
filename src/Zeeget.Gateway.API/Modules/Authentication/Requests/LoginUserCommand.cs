using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record LoginUserCommand : ICommand<Guid>
    {
        public UserLoginDto User { get; set; }

        public LoginUserCommand(UserLoginDto user)
        {
            User = user;
        }
    }
}
