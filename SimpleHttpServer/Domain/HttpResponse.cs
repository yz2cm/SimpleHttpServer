using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class HttpResponse
    {
        internal HttpResponse(HttpStatusLine httpStatusLine, HttpHeaderFieldEntryCollection httpHeaderFieldEntryCollection, HttpMessageBody httpMessageBody)
        {
            this.HttpStatusLine = httpStatusLine;
            this.HttpHeaderFieldEntries = httpHeaderFieldEntryCollection;
            this.HttpMessageBody = httpMessageBody;
        }
        internal HttpStatusLine HttpStatusLine;
        internal HttpHeaderFieldEntryCollection HttpHeaderFieldEntries;
        internal HttpMessageBody HttpMessageBody;
        internal byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }
        public override string ToString()
        {
            var httpHeaders = this.HttpHeaderFieldEntries;

            if (httpHeaders.ContentLengthValueIsBlank())
            {
                httpHeaders.SetContentLength(this.HttpMessageBody.ByteLength);
            }

            string serialized = this.HttpStatusLine + "\r\n" + string.Join("\r\n", httpHeaders) + "\r\n\r\n";
            if (this.HttpMessageBody.ByteLength > 0)
            {
                serialized += this.HttpMessageBody.ToString();
            }

            return serialized;
        }
    }
}
