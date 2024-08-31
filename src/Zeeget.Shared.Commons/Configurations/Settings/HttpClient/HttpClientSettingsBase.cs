namespace Zeeget.Shared.Configurations.Settings.HttpClient
{
    public class HttpClientSettingsBase
    {
        public HttpClientSettingsBase() { }

        public HttpClientSettingsBase(string baseAddress, string requestUri)
        {
            BaseAddress = baseAddress;
            RequestUri = requestUri;
        }

        public string? BaseAddress { get; set; }
        public string? RequestUri { get; set; }
    }
}
