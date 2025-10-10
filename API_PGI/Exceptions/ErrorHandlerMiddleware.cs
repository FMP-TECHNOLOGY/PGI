using Common.Exceptions;
using Common.Models;
using System.Net;

namespace API_PGI.Exceptions
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string msg;

                if (ex is CustomException cex)
                    msg = string.IsNullOrWhiteSpace(cex.DevMessage) ? ex.Message : cex.DevMessage;
                else
                    msg = ex.Message;

                _logger.LogError(ex, msg);
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var code = (int)HttpStatusCode.InternalServerError;

            CustomResponse response;

            if (exception is CustomException ex)
            {
                context.Response.StatusCode = ex.StatusCode;
                response = ex.ToCustomResponse();
            }
            else if (exception is AggregateException agex)
            {
                return HandleExceptionAsync(context, agex.InnerException);
                //if (agex.InnerException is CustomException cex)
                //{
                //    context.Response.StatusCode = cex.StatusCode;
                //    response = cex.ToCustomResponse();
                //}
                //else
                //{
                //    context.Response.StatusCode = 400;
                //    //context.Response.StatusCode = exception.InnerException.StatusCode;
                //    response = CustomResponse.Error(400, exception.InnerException?.Message);
                //}
            }
            else if (exception is NotImplementedException)
            {
                context.Response.StatusCode = 400;

                response = CustomResponse.Error(code, "An action has been called that doesn't have any programmed logic.");
            }
            else
            {
                context.Response.StatusCode = code;
                response = CustomResponse.Error(code, "An unexpected server error has occurred.");
            }

            return context.Response.WriteAsJsonAsync(response);
        }

    }

}
