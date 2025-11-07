using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public abstract class BaseException : Exception
    {
        public abstract int ErrorCode { get; set; }
        public abstract HttpStatusCode StatusCode { get; set; }

        public IEnumerable<string?> Errors { get; set; } = new List<string?>();
        public object? ObjResult { get; set; }
        public new IDictionary Data { get; set; } = new Dictionary<string, object>();

        public BaseException() { }

        public BaseException(string? message) : base(message) { }

        public BaseException(string? message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BaseException(string? message, Exception? innerException) : base(message, innerException) { }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
