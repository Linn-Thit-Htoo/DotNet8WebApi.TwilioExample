using DotNet8WebApi.TwilioExample.Enums;

namespace DotNet8WebApi.TwilioExample.Entities
{
    public class Result<T>
    {
        public T Data { get; set; }
        public EnumStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public static Result<T> Success(string message = "Success.")
        {
            return new Result<T>
            {
                Message = message,
                IsSuccess = true,
                StatusCode = EnumStatusCode.Success
            };
        }

        public static Result<T> Success(T data, string message = "Success.")
        {
            return new Result<T>
            {
                Data = data,
                Message = message,
                IsSuccess = true,
                StatusCode = EnumStatusCode.Success
            };
        }

        public static Result<T> Fail(
            string message = "Fail.",
            EnumStatusCode statusCode = EnumStatusCode.BadRequest
        )
        {
            return new Result<T>
            {
                Message = message,
                IsSuccess = false,
                StatusCode = statusCode
            };
        }

        public static Result<T> Fail(Exception ex)
        {
            return new Result<T>
            {
                Message = ex.ToString(),
                IsSuccess = false,
                StatusCode = EnumStatusCode.InternalServerError
            };
        }

        public static Result<T> Duplicate(string message = "Duplicate data.")
        {
            return Result<T>.Fail(message, EnumStatusCode.Conflict);
        }

        public static Result<T> NotFound(string message = "No data found.")
        {
            return Result<T>.Fail(message, EnumStatusCode.Conflict);
        }
    }
}
