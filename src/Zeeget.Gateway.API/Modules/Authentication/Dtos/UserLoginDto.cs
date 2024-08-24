namespace Zeeget.Gateway.API.Modules.Authentication.Dtos
{
    public record UserLoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
