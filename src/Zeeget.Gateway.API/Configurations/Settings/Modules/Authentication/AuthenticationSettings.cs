namespace Zeeget.Gateway.API.Configurations.Settings.Modules.Authentication
{
    public class AuthenticationSettings
    {
        public string? Authority { get; set; }
        public string? Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; }
    }
}
