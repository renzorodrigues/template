namespace Zeeget.Gateway.API.Modules.Authentication.Dtos
{
    public record UserRegistrationDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
