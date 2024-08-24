using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record LoginUserQuery : IQuery<string>
    {
        public UserLoginDto User { get; set; }

        public LoginUserQuery(UserLoginDto user)
        {
            User = user;
        }
    }
}
