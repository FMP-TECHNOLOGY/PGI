using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResponseModel
    {
        public int Rows { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public bool Error { get; set; }
        public int Code { get; set; }
        public string?Message { get; set; }
        public object Result { get; set; }

        public static ResponseModel GetErrorResponse()
        {
            return new ResponseModel()
            {
                Error = true
            };
        }

        public static ResponseModel GetErrorResponse(string? message, int code = -1)
        {
            return new ResponseModel()
            {
                Error = true,
                Code = code,
                Message = message
            };
        }

        public static ResponseModel GetForbbidenResponse(string? message = "Forbbiden")
        {
            return new ResponseModel()
            {
                Error = true,
                Code = StatusCodes.Status403Forbidden,
                Message = message
            };
        }

        public static ResponseModel GetUnauthorizedResponse(string? message = "Unauthorized")
        {
            return new ResponseModel()
            {
                Error = true,
                Code = StatusCodes.Status401Unauthorized,
                Message = message
            };
        }

        public static ResponseModel GetBadRequestResponse(string? message = "BadRequest")
        {
            return new ResponseModel()
            {
                Error = true,
                Code = StatusCodes.Status400BadRequest,
                Message = message
            };
        }
    }
}
