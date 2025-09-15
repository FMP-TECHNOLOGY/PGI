using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class BadRequestException : BaseException
    {
        public override int ErrorCode { get; set; } = (int)HttpStatusCode.BadRequest;
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public static string ErrorDescription { get; } = "Something went wrong";

        public BadRequestException() : base(ErrorDescription) { }

        public BadRequestException(string? message) : base(message ?? ErrorDescription) { }

        public BadRequestException(Exception? innerException) : base(ErrorDescription, innerException) { }

        public BadRequestException(string? message, Exception? innerException) : base(message ?? ErrorDescription, innerException) { }
    }
}
