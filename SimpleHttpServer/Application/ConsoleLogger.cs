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
            string LogLine(LogTimeStamp timeStamp, string message);
        }
        class LogContextFactory
        {
            internal static ILogContext Create(LogLevel logLevel)
            {
                if (logLevel == LogLevel.Info)
                    return new LogContext_Information();
                if (logLevel == LogLevel.Warn)
                    return new LogContext_Warning();
                if (logLevel == LogLevel.Error)
                    return new LogContext_Error();
                if (logLevel == LogLevel.Debug)
                    return new LogContext_Debug();
                if (logLevel == LogLevel.Notify)
                    return new LogContext_Notify();

                return new LogContext_Other();
            }
        }
        private class LogContext_Information : ILogContext
        {
            public ConsoleColor foreColor => ConsoleColor.Gray;
            public string LogLine(LogTimeStamp timeStamp, string message)
            {
                return $"{timeStamp} [{LogLevel.Info}] {message}";
            }
        }
        private class LogContext_Warning: ILogContext
        {
            public ConsoleColor foreColor => ConsoleColor.Yellow;
            public string LogLine(LogTimeStamp timeStamp, string message)
            {
                return $"{timeStamp} [{LogLevel.Warn}] {message}";
            }
        }
        private class LogContext_Error : ILogContext
        {
            public ConsoleColor foreColor => ConsoleColor.Yellow;
            public string LogLine(LogTimeStamp timeStamp, string message)
            {
                return $"{timeStamp} [{LogLevel.Error}] {message}";
            }
        }
        private class LogContext_Debug : ILogContext
        {
            public ConsoleColor foreColor { get; } = ConsoleColor.DarkGray;
            public string LogLine(LogTimeStamp timeStamp, string message)
            {
                return message;
            }
        }
        private class LogContext_Notify : ILogContext
        {
            public ConsoleColor foreColor { get; } = ConsoleColor.Green;
            public string LogLine(LogTimeStamp timeStamp, string message)
            {
                return $"{timeStamp} [{LogLevel.Notify}] {message}";
            }
        }
        private class LogContext_Other : ILogContext
        {
            public ConsoleColor foreColor { get; } = Console.ForegroundColor;
            public string LogLine(LogTimeStamp timeStamp, string message)
            {
                return $"{timeStamp} [{LogLevel.Other}] {message}";
            }
        }

        protected override void WriteCore(LogLevel logLevel, string message)
        {
            ILogContext context = LogContextFactory.Create(logLevel);

            var timeStamp = new LogTimeStamp();
            string log = context.LogLine(timeStamp, message);

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
