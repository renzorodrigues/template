using Zeeget.Shared.Api.CustomResponse;
using static Zeeget.Shared.Utils.Constants.ContantStrings;

namespace Zeeget.Shared.Api
{
    public interface IResult
    {
        SuccessResult<T> Success<T>(T data);
        CreatedResult Created(Guid data, string message = ResultMessages.Created);
        NotFoundResult NotFound(string message = ResultMessages.NotFound);
        UnauthorizedResult Unauthorized(string message = ResultMessages.Unauthorized);
        BadRequestResult BadRequest(
            IDictionary<string, string[]>? errors = null,
            string message = ResultMessages.BadRequest
        );
        ErrorResult Error(string message = ResultMessages.Error);
    }
}
