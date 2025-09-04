using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Utils.Helpers
{
    public class Log
    {
        private static readonly string? rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string? subDir = "Log";
        private static readonly string? pathSeparator = Path.DirectorySeparatorChar.ToString();
        private static readonly string? path = $@"{rootDir}{pathSeparator}{subDir}";
        private readonly string? fileName;
        public readonly string? logPath;

        private static readonly object lckObject = new();

        public Log()
        {
            fileName = $"log-{DateTime.Now:yyyyMMdd}.txt";
            logPath = $@"{path}{pathSeparator}{fileName}";
        }


        public void Debug(string? message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Debug(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Debug(string? message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Info(string? message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Info(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Info(string? message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Warning(string? message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Warning(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Warning(string? message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Error(string? message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Error(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Error(string? message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Critical(string? message)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Critical(Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public void Critical(string? message, Exception e)
        {
            try
            {
                Write(MethodBase.GetCurrentMethod().Name, message, e);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Write(string? type, string? message)
        {

            lock (lckObject)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string?[] log = { $"[{type.ToUpper()}] : {DateTime.Now} : {message}" };
                File.AppendAllLines(logPath, log);
            }

        }
        private void Write(string? type, Exception e)
        {

            lock (lckObject)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string?[] log = { $"[{type.ToUpper()}] : {DateTime.Now} : {e}\nINNER EXCEPTION\n{e.InnerException}" };
                File.AppendAllLines(logPath, log);
            }

        }
        private void Write(string? type, string? message, Exception e)
        {

            lock (lckObject)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string?[] log = { $"[{type.ToUpper()}] : {DateTime.Now} : {message}\n{e}" };
                File.AppendAllLines(logPath, log);
            }

        }
    }
}
