using LandAreaResolver.Logger.Interfaces;
using System;
using System.IO;
using System.Text;

namespace LandAreaResolver.Logger.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly string _datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private readonly string _logFilePath;

        public FileLogger()
        {
            string fileName = System.Configuration.ConfigurationManager.AppSettings["LogFileName"];
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public void Error(string message)
        {
            WriteLine("ERROR", message);
        }

        public void Debug(string message)
        {
            WriteLine("DEBUG", message);
        }

        public void Fatal(string message)
        {
            WriteLine("FATAL", message);
        }

        public void Info(string message)
        {
            WriteLine("INFO", message);
        }

        public void Warn(string message)
        {
            WriteLine("WARN", message);
        }

        private void WriteLine(string logHeader, string message)
        {
            if (File.Exists(_logFilePath))
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToString(_datetimeFormat)} {logHeader} {message}");
                }
            }
            else
            {
                using (var writer = new FileStream(_logFilePath, FileMode.Create))
                {
                    string logMessage = $"{DateTime.Now.ToString(_datetimeFormat)} {logHeader} {message}";
                    writer.Write(Encoding.UTF8.GetBytes( logMessage.ToCharArray()), 0, logMessage.Length);
                }
            }

        }
    }
}
