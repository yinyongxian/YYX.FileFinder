using System;
using System.Globalization;
using System.Reflection;
using log4net;
using log4net.Config;

namespace YYX.FileFinder
{
    public static class Log4Log
    {
        private static readonly ILog Log4 = LogManager.GetLogger("Log4");
        static Log4Log()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fileStream = assembly.GetManifestResourceStream("YYX.FileFinder.log4net.config");
                XmlConfigurator.Configure(fileStream);
            }
            catch (Exception)
            {
                // ignored
            }
        }


        public static void Debug(string message)
        {
            if (Log4 != null && Log4.IsDebugEnabled)
            {
                Log4.Debug(message);
            }
        }

        public static void Info(string message)
        {
            if (Log4 != null && Log4.IsInfoEnabled)
            {
                message = DateTime.Now +
                          Environment.NewLine +
                          message +
                          Environment.NewLine;

                Log4.Info(message);
            }
        }

        public static void Warn(string message)
        {
            if (Log4 != null && Log4.IsWarnEnabled)
            {
                Log4.Info(message);
            }
        }

        public static void Error(Exception ex)
        {
            if (Log4 != null && Log4.IsErrorEnabled)
            {
                Log4.Error(ex.Message, ex);
            }
        }

        public static void Error(string message)
        {
            if (Log4 != null && Log4.IsErrorEnabled)
            {
                Log4.Error(message);
            }
        }

        public static void Fatal(string message)
        {
            if (Log4 != null && Log4.IsFatalEnabled)
            {
                Log4.Fatal(message);
            }
        }
    }
}
