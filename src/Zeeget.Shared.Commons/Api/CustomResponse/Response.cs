using System.Net;

namespace Zeeget.Shared.Commons.Api.CustomResponse
{
    public class Response<T> : CustomResponse
    {
        public T? Data { get; set; }

        public static Response<T> RequestOk(
            T? data,
            HttpStatusCode statusCode = HttpStatusCode.OK,
            string message = "Request Successfully!"
        )
        {
            return new()
            {
                Data = data,
                Message = message,
                IsSuccess = true,
                StatusCode = statusCode,
            };
        }

        public static Response<T> RequestFailed(
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            string message = "Request Failed!",
            IEnumerable<Error>? errors = null
        )
        {
            return new()
            {
                Message = message,
                Errors = errors,
                IsSuccess = false,
                StatusCode = statusCode,
            };
        }
    }
}
