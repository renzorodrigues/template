using System.Text.Json;
using Zeeget.Shared.Configurations.Settings.HttpClient;
using Zeeget.Shared.Services.HttpClient.Interfaces;

namespace Zeeget.Shared.Services.HttpClient
{
    public class HttpClientFactory<TRequest, TResponse>(IHttpClientFactory httpClientFactory)
        : IHttpClient<TRequest, TResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private FormUrlEncodedContent? _content;
        private TRequest? _request;

        public async Task<TResponse> SendAsync(TRequest request, HttpClientSettingsBase settings)
        {
            _request = request;
            return await CreateClient(settings);
        }

        public async Task<TResponse> SendAsync(
            HttpClientSettingsBase settings,
            FormUrlEncodedContent content
        )
        {
            _content = content;
            return await CreateClient(settings);
        }

        public async Task<TResponse> SendAsync(
            TRequest request,
            HttpClientSettingsBase settings,
            FormUrlEncodedContent content
        )
        {
            _content = content;
            return await CreateClient(settings);
        }

        private async Task<TResponse> CreateClient(HttpClientSettingsBase settings)
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(settings.BaseAddress!);

            try
            {
                var response = await CreateRequest(client, settings);

                var content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content) && response.IsSuccessStatusCode)
                {
                    return (TResponse)(object)true;
                }

                return JsonDeserializer(content);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        private async Task<HttpResponseMessage> CreateRequest(
            System.Net.Http.HttpClient client,
            HttpClientSettingsBase settings
        )
        {
            Type type = typeof(TRequest);

            if (type == typeof(string[]))
            {
                var parameters = _request as string[];

                string requestUri;

                if (!(parameters is null || parameters.Length == 0))
                {
                    requestUri =
                        $"{client.BaseAddress}{string.Format(settings.RequestUri!, parameters!)}";
                }
                else
                {
                    requestUri = $"{client.BaseAddress}{settings.RequestUri}";
                }

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                return await client.SendAsync(httpRequestMessage);
            }
            else
            {
                HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, settings.RequestUri);

                if (!string.IsNullOrEmpty(settings.Token))
                {
                    httpRequestMessage.Headers.Add("Authorization", $"Bearer {settings.Token}");
                }

                httpRequestMessage.Content = _content;

                if (httpRequestMessage.Content is null)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = true
                    };

                    var json = JsonSerializer.Serialize(_request, options);
                    httpRequestMessage.Content = new StringContent(json, null, "application/json");
                }

                _content = null;

                return await client.SendAsync(httpRequestMessage);
            }
        }

        private static TResponse JsonDeserializer(string content)
        {
            var deserializedContent = JsonSerializer.Deserialize<TResponse>(content);

            return deserializedContent is null
                ? throw new HttpRequestException("Content is null")
                : deserializedContent;
        }
    }
}
