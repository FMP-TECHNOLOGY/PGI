using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;


namespace Common.Exceptions
{

    public class CustomException : Exception
    {
        public int StatusCode { get; set; } = 400;
        public string ErrorCode { get; set; } = "40x100";
        string? _DevMessage;
        public string DevMessage { get => _DevMessage ?? Message; set => _DevMessage = value; }

        public CustomException(int statusCode = 400, string message = "An unhandled error has occurred.", string errorCode = "40x000", string? DevError = null) : base(message)
        {
            StatusCode = statusCode < 400 ? 400 : statusCode;
            ErrorCode = errorCode;
            this.DevMessage = DevError ?? message;
        }

        public CustomException(string? message) : base(message) { }

        public CustomException(string? message, int errorCode) : base(message)
        {
            ErrorCode = errorCode.ToString();
        }

        public CustomException(string? message, Exception? innerException) : base(message, innerException) { }

        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public CustomResponse ToCustomResponse()
        {
            return CustomResponse.Error(StatusCode, Message, ErrorCode, DevMessage);
        }

        public ActionResult ToActionResult()
        {
            return new ObjectResult(this) { StatusCode = StatusCode };
        }

        public static implicit operator CustomException(CustomResponse res) =>
            new(res.StatusCode, res.Message ?? "An unhandled error has occurred.", res.ErrorCode ?? "40x000");

    }
}
