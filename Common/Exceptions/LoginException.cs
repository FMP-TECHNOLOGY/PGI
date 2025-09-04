using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class LoginException : Exception
    {
        public int ErrorCode { get; set; }

        public LoginException() : base("Invalid username or password")
        {

        }
        public LoginException(string? message) : base(message)
        {

        }

        public LoginException(string? message, int code) : base(message)
        {
            ErrorCode = code;
        }

        public LoginException(string? message, int code, Exception innerException) : base(message, innerException)
        {
            ErrorCode = code;
        }
    }
}
