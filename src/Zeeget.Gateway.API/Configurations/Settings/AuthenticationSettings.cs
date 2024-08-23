namespace Zeeget.Gateway.API.Configurations.Settings
{
    public class AuthenticationSettings
    {
        public string? Authority { get; set; }
        public string? Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
