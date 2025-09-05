using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
//using Common.Exceptions;
//using Models.DTO;
using Common.Exceptions;
using Model;

namespace Utils.Helpers
{
    public class ExceptionHandler
    {

        int statusCode = StatusCodes.Status400BadRequest;

        public string?GetMessage(Exception e, out int code)
        {

            code = new int();
            var message = "UNKNOWN_ERROR";

            switch (e)
            {
                case LoginException:
                    var loginException = e as LoginException;
                    code = loginException.ErrorCode;
                    message = loginException.Message;
                    break;
                default:
                    message = e.Message;
                    break;
            }

            return message;
        }

        public ResponseModel GetResponse(Exception e, out int statusCode)
        {
            var message = GetMessage(e, out int code);

            statusCode = this.statusCode;

            return ResponseModel.GetErrorResponse(message, code);
        }
    }
}
