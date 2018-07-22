using System;
using LandAreaResolver.Logger.Interfaces;

namespace LandAreaResolver.Logger.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

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
            WriteLine("WARN" , message);
        }

        private void WriteLine(string logHeader, string message)
        {
            Console.WriteLine($"{DateTime.Now.ToString(_datetimeFormat)} {logHeader} {message}");
        }
    }
}
