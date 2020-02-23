using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Application
{
    class ConsoleLogger : LoggerBase
    {
        interface ILogContext
        {
            ConsoleColor foreColor { get; }
            string LogLine(LogTimeStamp timeStamp, LogLevel logLevel, string message);
        }
        private class LogContext_Information : ILogContext
        {
            public ConsoleColor foreColor => ConsoleColor.Gray;
            public string LogLine(LogTimeStamp timeStamp, LogLevel logLevel, string message)
            {
                return $"{timeStamp} [{logLevel}] {message}";
            }
        }
        private class LogContext_Warning: ILogContext
        {
            public ConsoleColor foreColor => ConsoleColor.Yellow;
            public string LogLine(LogTimeStamp timeStamp, LogLevel logLevel, string message)
            {
                return $"{timeStamp} [{logLevel}] {message}";
            }
        }
        private class LogContext_Error : ILogContext
        {
            public ConsoleColor foreColor => ConsoleColor.Yellow;
            public string LogLine(LogTimeStamp timeStamp, LogLevel logLevel, string message)
            {
                return $"{timeStamp} [{logLevel}] {message}";
            }
        }
        private class LogContext_Debug : ILogContext
        {
            public ConsoleColor foreColor { get; } = ConsoleColor.DarkGray;
            public string LogLine(LogTimeStamp timeStamp, LogLevel logLevel, string message)
            {
                return message;
            }
        }
        private class LogContext_Other : ILogContext
        {
            public ConsoleColor foreColor { get; } = Console.ForegroundColor;
            public string LogLine(LogTimeStamp timeStamp, LogLevel logLevel, string message)
            {
                return $"{timeStamp} [{logLevel}] {message}";
            }
        }

        protected override void WriteCore(LogLevel logLevel, string message)
        {
            ILogContext context;

            if(logLevel == LogLevel.Info)
                context = new LogContext_Information();
            else if(logLevel == LogLevel.Warn)
                context = new LogContext_Warning();
            else if (logLevel == LogLevel.Error)
                context = new LogContext_Error();
            else if (logLevel == LogLevel.Debug)
                context = new LogContext_Debug();
            else
                context = new LogContext_Other();

            var timeStamp = new LogTimeStamp();
            string log = context.LogLine(timeStamp, logLevel, message);

            if(Console.ForegroundColor == context.foreColor)
            {
                Console.WriteLine(log);
            }
            else
            {
                Console.ForegroundColor = context.foreColor;
                Console.WriteLine(log);
                Console.ResetColor();
            }
        }
    }
}
