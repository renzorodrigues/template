namespace Zeeget.Shared.Services.HttpRequest.Interfaces
{
    public interface IHttpService
    {
        IHttpRequestBuilder CreateRequest(string clientName = "default");
    }
}
