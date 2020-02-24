using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Application
{
    abstract class LoggerBase
    {
        protected enum LogLevel
        {
            Info,
            Warn,
            Error,
            Debug,
            Notify,
            Other,
        }

        protected abstract void WriteCore(LogLevel logLevel, string message);
        internal void WriteInformation(string message)
        {
            this.WriteCore(LogLevel.Info, message);
        }
        internal void WriteWarning(string message)
        {
            this.WriteCore(LogLevel.Warn, message);
        }
        internal void WriteError(string message)
        {
            this.WriteCore(LogLevel.Error, message);
        }
        internal void WriteDebug(string message)
        {
            this.WriteCore(LogLevel.Debug, message);
        }
        internal void WriteNotification(string message)
        {
            this.WriteCore(LogLevel.Notify, message);
        }
    }
}
