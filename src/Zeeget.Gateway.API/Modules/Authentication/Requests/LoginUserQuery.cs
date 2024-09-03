using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Shared.Api;
using Zeeget.Shared.Commons.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record LoginUserQuery : IQuery<Result>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginUserQuery(UserLoginDto user)
        {
            Username = user.Username;
            Password = user.Password;
        }
    }
}
