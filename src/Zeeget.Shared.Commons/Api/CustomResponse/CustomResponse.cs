using System.Net;

namespace Zeeget.Shared.Commons.Api.CustomResponse
{
    public class CustomResponse
    {
        public HttpStatusCode? StatusCode { get; set; } = new HttpResponseMessage().StatusCode;

        public bool IsSuccess { get; set; } = new HttpResponseMessage().IsSuccessStatusCode;

        public string? Message { get; set; }

        public IEnumerable<Error>? Errors { get; set; }
    }
}
