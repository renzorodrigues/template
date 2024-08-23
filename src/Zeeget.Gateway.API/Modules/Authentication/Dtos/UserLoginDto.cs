namespace Zeeget.Gateway.API.Modules.Authentication.Dtos
{
    public record UserLoginDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
