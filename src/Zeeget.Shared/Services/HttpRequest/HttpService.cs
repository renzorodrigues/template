using Zeeget.Shared.Services.HttpRequest.Interfaces;

namespace Zeeget.Shared.Services.HttpRequest
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IHttpRequestBuilder CreateRequest(string clientName = "default")
        {
            var httpClient = _httpClientFactory.CreateClient(clientName);
            return new HttpRequestBuilder(httpClient);
        }
    }
}
