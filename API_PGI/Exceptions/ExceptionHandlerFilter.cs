using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Utils.Helpers;

namespace API_PGI.Exceptions
{
    public class ExceptionHandlerFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {

            try
            {

                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;
                var headers = request.Headers;

                // Building response
                response.ContentType = "application/json; charset=utf-8";
                context.Result = new JsonResult(
                        new ExceptionHandler().GetResponse(context.Exception, out int statusCode)
                    );
                response.StatusCode = statusCode;


                // Getting info about request to build the log file. 
                var log = new StringBuilder("\r\n");
                log.AppendLine($"RemoteIpAddress : {context.HttpContext.Connection.RemoteIpAddress}");
                log.AppendLine($"Path : {request.Path.Value}");
                log.AppendLine($"Method : {request.Method}");
                log.AppendLine($"Scheme : {request.Scheme}");
                log.AppendLine($"QueryString : {request.QueryString.Value}");
                log.AppendLine("====================== HEADERS ======================");

                foreach (var item in headers)
                {
                    log.AppendLine($"{item.Key} : {item.Value}");
                }

                log.AppendLine("==================== END HEADERS ====================");

                /*MemoryStream memoryStream = new();
                await request.Body.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (StreamReader reader = new(memoryStream,//request.BodyReader.AsStream(),
                        Encoding.UTF8))
                {

                    log.AppendLine();
                    log.AppendLine("===================== BODY =======================");

                    if (reader.BaseStream.CanSeek && reader.EndOfStream)
                    {
                        reader.BaseStream.Seek(0, SeekOrigin.Begin);
                        reader.DiscardBufferedData();
                    }

                    log.AppendLine(reader.ReadToEnd());

                    log.AppendLine("==================== END BODY ====================");
                }*/

                LogData.Error(log.ToString().Trim(), context.Exception);
            }
            catch (Exception ex)
            {
                LogData.Error(ex);
            }

            context.ExceptionHandled = true;
        }

    }
}
