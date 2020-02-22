using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpRequestLine
    {
        internal HttpRequestLine(string requestLine)
        {
            this.requestLine = requestLine;

            var x = this.requestLine.Split(' ');
            this.HttpMethod = x[0];
            {
                var y = x[1].Split('?');
                this.RequestUri = y[0];
                if(y.Length >= 2)
                {
                    this.QueryString = y[1];
                }
                else
                {
                    this.QueryString = string.Empty;
                }
            }
            this.RequestUri = x[1];
            this.HttpVersion = x[2];
        }
        internal string HttpMethod { get; }
        internal string RequestUri { get; }
        internal string QueryString { get; }
        internal string HttpVersion { get; }
        private string requestLine;
        public override string ToString()
        {
            return this.requestLine;
        }
    }
}
