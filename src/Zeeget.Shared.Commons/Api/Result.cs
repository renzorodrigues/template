﻿using Zeeget.Shared.Api.CustomResponse;

namespace Zeeget.Shared.Api
{
    public class Result : IResult
    {
        public string? Message { get; set; }
        public string? StatusCode { get; set; }
        public bool IsSuccess { get; set; }

        public SuccessResult<T> Success<T>(T data)
        {
            return new SuccessResult<T>(data);
        }

        public CreatedResult Created(Guid data, string message)
        {
            return new CreatedResult(data, message);
        }

        public NotFoundResult NotFound(string message)
        {
            return new NotFoundResult(message);
        }

        public UnauthorizedResult Unauthorized(string message)
        {
            return new UnauthorizedResult(message);
        }

        public ErrorResult Error(string message)
        {
            return new ErrorResult(message);
        }

        public BadRequestResult BadRequest(string message)
        {
            return new BadRequestResult(message);
        }
    }
}
