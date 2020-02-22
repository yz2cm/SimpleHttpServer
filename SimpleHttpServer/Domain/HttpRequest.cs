using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpRequest
    {
        internal HttpRequest(string httpRequest)
        {
            var headerAndBody = httpRequest.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            string header = headerAndBody[0];
            string[] headerLines = header.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            string requestLine = headerLines[0];
            string headerSection = headerLines[1];

            this.HttpRequestLine = new HttpRequestLine(requestLine);
            this.HttpHeader = new HttpHeader(headerSection);

            if(headerAndBody.Length >= 2)
            {
                this.HttpMessageBody = new HttpMessageBody(headerAndBody[1]);
            }
            else
            {
                this.HttpMessageBody = new HttpMessageBody(string.Empty);
            }
        }
        internal HttpRequestLine HttpRequestLine { get; }
        internal HttpHeader HttpHeader { get; }
        internal HttpMessageBody HttpMessageBody { get; }
    }
}
