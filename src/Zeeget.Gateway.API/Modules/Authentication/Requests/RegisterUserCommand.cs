using Zeeget.Gateway.API.Modules.Authentication.Dtos;
using Zeeget.Gateway.API.Modules.Authentication.Models;
using Zeeget.Shared.Api;
using Zeeget.Shared.Handlers.Interfaces;

namespace Zeeget.Gateway.API.Modules.Authentication.Requests
{
    public record RegisterUserCommand : ICommand<Result>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Enabled { get; set; }
        public IEnumerable<Credential> Credentials { get; set; }

        public RegisterUserCommand(UserRegistrationDto user)
        {
            Username = user.Username;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Credentials = [new() { Type = "password", Value = user.Password, }];
        }
    }
}
