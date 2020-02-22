using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpStatusLine
    {
        internal HttpStatusLine(string statusLine)
        {
            this.statusLine = statusLine;

            var xs = this.statusLine.Split(' ');

            this.HttpVersion = xs[0];
            this.StatusCode = xs[1];
            this.StatusMessage = xs[2];
        }
        internal string HttpVersion { get; }
        internal string StatusCode { get; }
        internal string StatusMessage { get; }
        private string statusLine;
        public override string ToString()
        {
            return this.statusLine;
        }
    }
}
