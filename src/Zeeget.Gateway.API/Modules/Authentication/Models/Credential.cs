namespace Zeeget.Gateway.API.Modules.Authentication.Models
{
    public record Credential
    {
        public string? Type { get; set; }
        public string? Value { get; set; }
        public bool Temporary { get; set; }
    }
}
