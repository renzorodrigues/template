using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Api;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record LoginUserQuery : IQuery<Result>
    {
        public UserLoginDto User { get; set; }

        public LoginUserQuery(UserLoginDto user)
        {
            User = user;
        }
    }
}
