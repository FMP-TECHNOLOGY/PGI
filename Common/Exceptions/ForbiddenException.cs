using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public override int ErrorCode { get; set; } = (int)HttpStatusCode.Forbidden;
        public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.Forbidden;
        public static string ErrorDescription { get; } = "Access not allowed";


        public ForbiddenException() : base(ErrorDescription) { }

        public ForbiddenException(string message) : base(message ?? ErrorDescription) { }

        public ForbiddenException(string message, Exception innerException) : base(message ?? ErrorDescription, innerException) { }
    }
}
