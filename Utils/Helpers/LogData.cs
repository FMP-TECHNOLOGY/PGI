using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Utils.Helpers
{
    public static class LogData
    {
        private static readonly string rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        private static readonly string subDir = "Log";
        public static readonly string path = Path.Combine(rootDir, subDir);
        private static readonly object lockObj = new();

        private static string GetLogFullPath() => Path.Combine(path, $"log-{DateTime.Now:yyyyMMdd}.txt");

        public static async Task LogRequest(HttpContext context, string? requestId = null)
        {
            try
            {
                var request = context.Request;
                var response = context.Response;
                var headers = request.Headers;

                var requestLang = headers.TryGetValue("Accept-Language", out StringValues values)
                    ? values.SingleOrDefault()?
                            .Replace("_", "-")?
                            .Split("-", 2, StringSplitOptions.RemoveEmptyEntries)?
                            .FirstOrDefault()?
                .ToLower()
                    : "en";

                // Getting info about request to build the log file. 
                var log = new StringBuilder();
                log.AppendLine();
                log.AppendLine($"RequestId : {requestId}");
                log.AppendLine($"RemoteIpAddress : {context.Connection.RemoteIpAddress}");
                log.AppendLine($"Path : {request.Path.Value}");
                log.AppendLine($"Method : {request.Method}");
                log.AppendLine($"Scheme : {request.Scheme}");
                log.AppendLine($"QueryString : {request.QueryString.Value}");
                log.AppendLine($"ContentType : {request.ContentType}");

                log.AppendLine("====================== HEADERS ======================");
                foreach (var item in headers)
                    log.AppendLine($"{item.Key} : {item.Value}");
                log.AppendLine("==================== END HEADERS ====================");

                if (request.HasFormContentType && request.Form != null)
                {
                    log.AppendLine();
                    log.AppendLine("===================== FORM =======================");

                    var formData = await request.ReadFormAsync();

                    foreach (var item in formData)
                        log.AppendLine($"{item.Key} : {item.Value}");

                    if (request.Form.Files is not null && request.Form.Files.Count > 0)
                    {
                        foreach (var file in request.Form.Files)
                        {
                            log.AppendLine();
                            log.AppendLine("===================== FILE =======================");

                            log.AppendLine($"ContentType : {file.ContentType}")
                               .AppendLine($"FormName : {file.Name}")
                               .AppendLine($"FileName : {file.FileName}")
                               .AppendLine($"Length : {file.Length}");

                            log.AppendLine("================= FILE CONTENT ===================");
                            TryLogFile(file, log);
                            log.AppendLine("================= FILE CONTENT ===================");

                            log.AppendLine("==================== END FILE ====================");
                        }
                    }

                    log.AppendLine("==================== END FORM ====================");
                }

                var bodyIsReadble = (request.ContentType ?? "").Contains("json", StringComparison.CurrentCultureIgnoreCase)
                    || (request.ContentType ?? "").Contains("xml", StringComparison.CurrentCultureIgnoreCase);

                if (bodyIsReadble)
                {
                    log.AppendLine();
                    log.AppendLine("===================== BODY =======================");

                    log.AppendLine(await ReadBodyAsStringAsync(request));

                    log.AppendLine("==================== END BODY ====================");
                }

                Info(log.ToString());
            }
            catch (Exception e)
            {
                Error(e);
            }
        }

        private static void TryLogFile(IFormFile file, StringBuilder log)
        {
            Stream? reader = null;
            try
            {
                reader = file.OpenReadStream();

                //if (Path.GetExtension(file.FileName).Contains("xml", StringComparison.InvariantCultureIgnoreCase))
                //    log.AppendLine(reader.ToXmlString(false));

                if (Path.GetExtension(file.FileName).Contains("json", StringComparison.InvariantCultureIgnoreCase))
                    log.AppendLine(reader.ToJsonString());
                else
                    log.AppendLine($"{file.FileName}: invalid content-type || {file.ContentType}");
            }
            catch (Exception ex)
            {
                log.AppendLine($"ERROR: {ex}");
            }
            finally
            {
                reader?.Seek(0, SeekOrigin.Begin);
            }
        }

        private static async Task<string> ReadBodyAsStringAsync(HttpRequest request)
        {
            try
            {
                var bodyString = string.Empty;

                var reader = new StreamReader(request.Body, Encoding.UTF8);

                ResetStream(reader);

                var body = await reader.ReadToEndAsync();

                ResetStream(reader);

                return body;
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
        }

        private static void ResetStream(StreamReader reader)
        {
            if (reader.BaseStream.CanSeek && reader.EndOfStream)
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                reader.DiscardBufferedData();
            }
        }

        public static void Debug(string message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Debug(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Debug(string message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Info(string message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Info(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Info(string message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Warning(string message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Warning(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Warning(string message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Error(string message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Error(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Error(string message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Critical(string message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Critical(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void Critical(string message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod()?.Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Custom(string content, string? pathName = null)
        {
            lock (lockObj)
            {
                pathName ??= GetLogFullPath();

                var dir = Path.GetDirectoryName(pathName)
                    ?? throw new IOException($"Invalid pathName {pathName}");

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var builder = new StringBuilder()
                         .AppendLine($"[CUSTOM] : {DateTime.Now} :")
                         .AppendLine("===================== LOG =======================")
                         .AppendLine(content)
                         .AppendLine("=================== END LOG =====================")
                         .AppendLine();

                File.AppendAllText(pathName, builder.ToString());
            }
        }

        private static void Write(string? type, string message)
        {
            lock (lockObj)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string[] log = { $"[{type?.ToUpper()}] : {DateTime.Now} : {message}" };
                File.AppendAllLines(GetLogFullPath(), log);
            }
        }
        private static void Write(string? type, Exception e)
            => Write(type, "", e);

        private static void Write(string? type, string message, Exception e)
        {
            lock (lockObj)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var builder = new StringBuilder()
                          .AppendLine($"[{type?.ToUpper()}] : {DateTime.Now} :")
                          .AppendLine(message)
                          .AppendLine("===================== EXCEPTION =======================")
                          .AppendLine(e.ToString())
                          .AppendLine("===================== END EXCEPTION =======================");

                try
                {
                    builder.AppendLine("===================== JEXCEPTION =======================")
                           .AppendLine(JsonSerializer.Serialize(e, new JsonSerializerOptions()
                           {
                               IgnoreReadOnlyFields = true,
                               IgnoreReadOnlyProperties = true,
                               ReferenceHandler = ReferenceHandler.IgnoreCycles
                           }))
                           .AppendLine("===================== END JEXCEPTION =======================");
                }
                catch { }

                var innerException = e.InnerException;

                do
                {
                    builder.AppendLine("===================== INNER EXCEPTION =======================")
                          .AppendLine(e.InnerException?.ToString())
                          .AppendLine("===================== END INNER EXCEPTION =======================");

                    innerException = innerException?.InnerException;

                } while (innerException != null);

                File.AppendAllText(GetLogFullPath(), builder.ToString());
            }
        }
    }
}
