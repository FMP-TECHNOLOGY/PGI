using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public override int ErrorCode { get; set; } = (int)HttpStatusCode.Unauthorized;
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Unauthorized;
        public static string ErrorDescription { get; } = "Unauthorized";

        public UnauthorizedException() : base(ErrorDescription) { }

        public UnauthorizedException(string? message) : base(message ?? ErrorDescription) { }

        public UnauthorizedException(string? message, Exception innerException) : base(message ?? ErrorDescription, innerException) { }
    }
}
