namespace Zeeget.Shared.Configurations.Settings.HttpClient
{
    public class HttpClientSettingsBase
    {
        public HttpClientSettingsBase() { }

        public string? BaseAddress { get; set; }
        public string? RequestUri { get; set; }
        public string? Token { get; set; }
    }
}
