using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class CustomResponse
    {
        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; } = null;
        [JsonIgnore] public string? DevMessage { get; set; }
        public string? ErrorCode { get; set; } = null;
        protected CustomResponse() { }
        public CustomResponse(bool success, int statusCode, string? message = null, string? errorCode = null, string? devMsg = null)
        {
            this.Success = success;
            this.StatusCode = statusCode;
            this.Message = message;
            this.ErrorCode = errorCode;
            this.DevMessage = devMsg;
        }
        public ActionResult ToActionResult()
        {
            return new ObjectResult(this) { StatusCode = (StatusCode == 204) ? 200 : StatusCode };
        }

        public CustomException ToExeption()
        {
            return new CustomException(statusCode: StatusCode,
            errorCode: ErrorCode ?? "unknown",
            message: Message ?? "An unhandled exception has occurred");
        }

        public static CustomResponse SuccessResponse(int statusCode = 200)
        {
            return new CustomResponse(true, statusCode);
        }

        public static CustomResponse Error(int statusCode = 400, string? message = null, string? errorCode = null, string? devMsg = null)
        {
            return new CustomResponse(false, statusCode, message, errorCode, devMsg);
        }


        public static CustomResponse ProcessMultiple(IEnumerable<Task<CustomResponse>> responses) => ProcessMultiple(responses.Select(x => x.Result));
        public static CustomResponse ProcessMultiple(IEnumerable<CustomResponse> responses)
        {
            if (responses.Any(x => !x.Success))
            {
                var message = String.Join(" | ", responses
                        .Where(x => !string.IsNullOrWhiteSpace(x.Message))
                        .Select(x => $"{x.Message} (Code: {x.ErrorCode})."));

                if (responses.Any(x => x.Success))
                    return new CustomResponse(true, 207, message);

                return CustomResponse.Error(400, message);

            }

            return CustomResponse.SuccessResponse(200);
        }

        public static implicit operator CustomResponse(CustomException ex) => new(false, ex.StatusCode, ex.Message, ex.ErrorCode);

        public static implicit operator ActionResult(CustomResponse cr) => cr.ToActionResult();

    }

    public class CustomResponse<T> : CustomResponse
    {
        public int Count { get; set; }
        public int PageSize { get; set; } = 20;
        public int Page { get; set; } = 1;
        public T? Data { get; set; }

        public CustomResponse()
        {

        }
        public CustomResponse(bool success, int statusCode, string? message = null, T? data = default, string? errorCode = null, int count = 0, int page = 1, int pageSize = 20)
            : base(success, statusCode, message, errorCode)
        {
            Data = data;

            Page = page;
            PageSize = pageSize;
            Count = count;
        }

        public CustomResponse(CustomResponse parent)
        {
            this.ErrorCode = parent.ErrorCode;
            this.Message = parent.Message;
            this.Success = parent.Success;
            this.StatusCode = parent.StatusCode;
            this.Data = default;
        }
        // Métodos estáticos para conveniencia
        public static CustomResponse<T> SuccessResponse<T>(int statusCode, T data, string? message = null)
        {
            return new CustomResponse<T>(true, statusCode, message, data);
        }

        public static new CustomResponse<T> ErrorResponse<T>(int statusCode, string? message = null, string? errorCode = null)
        {
            return new CustomResponse<T>(false, statusCode, message, default, errorCode);
        }

        public static implicit operator CustomResponse<T>(CustomException ex) => new(false, ex.StatusCode, ex.Message, default, ex.ErrorCode);

        public static implicit operator ActionResult(CustomResponse<T> cr) => cr.ToActionResult();
    }
}
