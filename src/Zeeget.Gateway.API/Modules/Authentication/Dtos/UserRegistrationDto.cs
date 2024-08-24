namespace Zeeget.Gateway.API.Modules.Authentication.Dtos
{
    public record UserRegistrationDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}
