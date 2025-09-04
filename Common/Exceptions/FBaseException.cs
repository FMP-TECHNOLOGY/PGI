using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class FBaseException : Exception
    {
        public IEnumerable<string?> Messages { get; set; }
        public int StatusCode { get; set; }

        public FBaseException(string? message) : base(message)
        {

        }
        public FBaseException(IEnumerable<string?> messages) : base(string?.Join(" | ", messages))
        {
            Messages = messages;
        }

        public FBaseException(IEnumerable<string?> messages, int statusCode) : base(string?.Join(" | ", messages))
        {
            Messages = messages;
            StatusCode = statusCode;
        }
    }
}
