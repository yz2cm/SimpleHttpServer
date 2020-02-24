using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpRequest
    {
        internal HttpRequest(HttpRequestLine httpRequestLine, HttpHeaderFieldEntryCollection httpHeaderCollection, HttpMessageBody httpMessageBody)
        {
            this.HttpRequestLine = httpRequestLine;
            this.HttpHeaderCollection = httpHeaderCollection;
            this.HttpMessageBody = httpMessageBody;
        }
        internal HttpRequestLine HttpRequestLine { get; }
        internal HttpHeaderFieldEntryCollection HttpHeaderCollection { get; }
        internal HttpMessageBody HttpMessageBody { get; }
    }
}
