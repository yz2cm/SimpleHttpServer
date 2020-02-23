using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Application
{
    class LogTimeStamp
    {
        internal LogTimeStamp() : this(DateTime.Now) { }
        internal LogTimeStamp(DateTime dt)
        {
            this.dt = dt;
        }
        internal LogTimeStamp Utc()
        {
            return new LogTimeStamp(dt.ToUniversalTime());
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.dt.Hour.ToString("00"));
            sb.Append(":");
            sb.Append(this.dt.Minute.ToString("00"));
            sb.Append(":");
            sb.Append(this.dt.Second.ToString("00"));
            sb.Append(".");
            sb.Append(this.dt.Millisecond.ToString("000"));

            return sb.ToString();
        }
        private DateTime dt;
    }
}
