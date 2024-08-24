using System.Net;
using Zeeget.Shared.Commons.Api.CustomResponse;

namespace Zeeget.Shared.Commons.Utils.Validations
{
    public class ValidationException(string message) : Exception(message)
    {
        public static IList<Error> Errors { get; set; } = [];
        public static HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
    }
}
