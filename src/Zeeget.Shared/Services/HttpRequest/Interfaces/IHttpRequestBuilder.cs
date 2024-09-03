namespace Zeeget.Shared.Services.HttpRequest.Interfaces
{
    public interface IHttpRequestBuilder
    {
        IHttpRequestBuilder WithMethod(HttpMethod method);
        IHttpRequestBuilder WithRequestUri(string uri);
        IHttpRequestBuilder WithHeader(string name, string value);
        IHttpRequestBuilder WithQueryParam(string name, string value);
        IHttpRequestBuilder WithContent(HttpContent content);
        IHttpRequestBuilder WithStringContent<T>(T content);
        IHttpRequestBuilder WithFormUrlEncodedContent(Dictionary<string, string> formData);
        Task<HttpResponseMessage> SendAsync();
        Task<T?> SendAsync<T>();
        Task<(T? Data, HttpResponseMessage Response)> SendWithHttpResponseAsync<T>();
    }
}
