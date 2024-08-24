using System.Net.Http;
using System.Text.Json;
using Zeeget.Shared.Commons.Configurations.Settings.HttpClient;
using Zeeget.Shared.Commons.Services.HttpClient.Interfaces;

namespace Zeeget.Shared.Commons.Services.HttpClient
{
    public class HttpClientFactory<TRequest, TResponse>(IHttpClientFactory httpClientFactory)
        : IHttpClient<TRequest, TResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private FormUrlEncodedContent? _formContent;

        public async Task<TResponse> SendAsync(TRequest request, HttpClientSettingsBase urls)
        {
            return await CreateClient(request, urls);
        }

        public async Task<TResponse> SendAsync(
            TRequest request,
            HttpClientSettingsBase urls,
            FormUrlEncodedContent formContent
        )
        {
            _formContent = formContent;
            return await CreateClient(request, urls);
        }

        private async Task<TResponse> CreateClient(
            TRequest request,
            HttpClientSettingsBase urls
        )
        {
            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(urls.BaseAddress!);

            try
            {
                var response = await CreateRequest(request, client, urls);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TResponse>(content)!;
                }
                else
                {
                    throw new HttpRequestException($"{response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }

        private async Task<HttpResponseMessage> CreateRequest(
            TRequest request,
            System.Net.Http.HttpClient client,
            HttpClientSettingsBase urls
        )
        {
            Type type = typeof(TRequest);

            if (type == typeof(string[])) // GET
            {
                var parameters = request as string[];

                string requestUri;

                if (!(parameters is null || parameters.Length == 0))
                {
                    requestUri =
                        $"{client.BaseAddress}{string.Format(urls.RequestUri!, parameters!)}";
                }
                else
                {
                    requestUri = $"{client.BaseAddress}{urls.RequestUri}";
                }

                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                return await client.SendAsync(httpRequestMessage);
            }
            else // POST
            {
                var httpRequestMessage = new HttpRequestMessage(
                    HttpMethod.Post,
                    urls.RequestUri
                )
                {
                    Content = _formContent
                };

                if (httpRequestMessage.Content is null)
                {
                    var json = JsonSerializer.Serialize(request);

                    httpRequestMessage.Content = new StringContent(json, null, "application/json");
                }

                return await client.SendAsync(httpRequestMessage);
            }
        }
    }
}
