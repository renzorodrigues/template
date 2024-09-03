using System.Text.Json;
using Zeeget.Shared.Services.HttpRequest.Interfaces;

namespace Zeeget.Shared.Services.HttpRequest
{
    public class HttpRequestBuilder : IHttpRequestBuilder
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _request;
        private readonly List<KeyValuePair<string, string>> _queryParams;

        public HttpRequestBuilder(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _request = new HttpRequestMessage();
            _queryParams = [];
        }

        public IHttpRequestBuilder WithMethod(HttpMethod method)
        {
            _request.Method = method;
            return this;
        }

        public IHttpRequestBuilder WithRequestUri(string uri)
        {
            var uriBuilder = new UriBuilder(uri);
            if (_queryParams.Count > 0)
            {
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var param in _queryParams)
                {
                    query[param.Key] = param.Value;
                }
                uriBuilder.Query = query.ToString();
            }
            _request.RequestUri = uriBuilder.Uri;
            return this;
        }

        public IHttpRequestBuilder WithHeader(string name, string value)
        {
            _request.Headers.Add(name, value);
            return this;
        }

        public IHttpRequestBuilder WithQueryParam(string name, string value)
        {
            _queryParams.Add(new KeyValuePair<string, string>(name, value));
            return this;
        }

        public IHttpRequestBuilder WithContent(HttpContent content)
        {
            _request.Content = content;
            return this;
        }

        public IHttpRequestBuilder WithStringContent<T>(T content)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(content, options);
            _request.Content = new StringContent(json, null, "application/json");
            return this;
        }

        public IHttpRequestBuilder WithFormUrlEncodedContent(Dictionary<string, string> formData)
        {
            _request.Content = new FormUrlEncodedContent(formData);
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            return await _httpClient.SendAsync(_request);
        }

        public async Task<T?> SendAsync<T>()
        {
            var response = await _httpClient.SendAsync(_request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return string.IsNullOrWhiteSpace(content)
                ? default
                : JsonSerializer.Deserialize<T>(content);
        }

        public async Task<(T? Data, HttpResponseMessage Response)> SendWithHttpResponseAsync<T>()
        {
            var response = await _httpClient.SendAsync(_request);
            var content = await response.Content.ReadAsStringAsync();

            T? data = !string.IsNullOrWhiteSpace(content)
                ? JsonSerializer.Deserialize<T>(content)
                : default;

            return (data, response);
        }
    }
}
