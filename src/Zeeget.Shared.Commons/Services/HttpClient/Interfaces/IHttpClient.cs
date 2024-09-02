using Zeeget.Shared.Configurations.Settings.HttpClient;

namespace Zeeget.Shared.Services.HttpClient.Interfaces
{
    public interface IHttpClient<TRequest, TResponse>
    {
        Task<TResponse> SendAsync(TRequest request, HttpClientSettingsBase settings);
        Task<TResponse> SendAsync(TRequest request, HttpClientSettingsBase settings, FormUrlEncodedContent content);
        Task<TResponse> SendAsync(HttpClientSettingsBase settings, FormUrlEncodedContent content);
    }
}
