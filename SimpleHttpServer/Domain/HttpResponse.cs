using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class HttpResponse
    {
        internal HttpResponse(HttpResponseFileDto responseFileDto)
        {
            this.responseFileDto = responseFileDto;
            this.HttpStatusLine = new HttpStatusLine(this.responseFileDto.HttpStatusLine);
            this.HttpHeaderFieldEntries = new HttpHeaderFieldEntryCollection(this.responseFileDto.HttpHeaderFieldEntries);
            this.HttpMessageBody = new HttpMessageBody(this.responseFileDto.HttpMessageBody);
        }
        private HttpResponseFileDto responseFileDto;
        internal HttpStatusLine HttpStatusLine;
        internal HttpHeaderFieldEntryCollection HttpHeaderFieldEntries;
        internal HttpMessageBody HttpMessageBody;
        internal byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }
        public override string ToString()
        {
            var httpHeaders = new HttpHeaderFieldEntryCollection(this.responseFileDto.HttpHeaderFieldEntries);

            if (httpHeaders.ContentLengthValueIsBlank())
            {
                httpHeaders.SetContentLength(this.HttpMessageBody.ByteLength);
            }

            string serialized = this.responseFileDto.HttpStatusLine + "\r\n" + string.Join("\r\n", httpHeaders) + "\r\n\r\n";
            if (this.HttpMessageBody.ByteLength > 0)
            {
                serialized += this.HttpMessageBody.ToString();
            }

            return serialized;
        }
    }
}
