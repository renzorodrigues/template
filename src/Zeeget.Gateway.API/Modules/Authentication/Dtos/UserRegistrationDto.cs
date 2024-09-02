﻿namespace Zeeget.Gateway.API.Modules.Authentication.Dtos
{
    public record UserRegistrationDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
    }
}
