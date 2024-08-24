using Zeeget.Shared.Commons.Configurations.Settings.HttpClient;

namespace Zeeget.Shared.Commons.Services.HttpClient.Interfaces
{
    public interface IHttpClient<TRequest, TResponse>
    {
        Task<TResponse> SendAsync(TRequest request, HttpClientSettingsBase settings);
        Task<TResponse> SendAsync(TRequest request, HttpClientSettingsBase settings, FormUrlEncodedContent content);
    }
}
