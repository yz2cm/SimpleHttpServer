using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class HttpRequest
    {
        internal HttpRequest(HttpRequestMessageDto httpRequestDto)
        {
            this.HttpRequestLine = new HttpRequestLine(httpRequestDto.HttpRequestLine);
            this.HttpHeader = new HttpHeaderFieldEntryCollection(httpRequestDto.HttpHeaderFields);
            this.HttpMessageBody = new HttpMessageBody(httpRequestDto.HttpMessageBody);
        }
        internal HttpRequestLine HttpRequestLine { get; }
        internal HttpHeaderFieldEntryCollection HttpHeader { get; }
        internal HttpMessageBody HttpMessageBody { get; }
    }
}
