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
        internal HttpResponse(HttpResponseFile responseFile)
        {
            string serializedData = responseFile.GetFileContent();
            string[] headerAndBody = serializedData.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            string headerSection = headerAndBody[0];
            string[] headerLines = headerSection.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            this.HttpStatusLine = new HttpStatusLine(headerLines[0]);

            IReadOnlyList<string> headerFieldLines = headerLines.Skip(1).ToList();
            this.HttpHeader = new HttpHeader(headerFieldLines);

            if(headerAndBody.Length >= 2)
            {
                this.HttpMessageBody = new HttpMessageBody(headerAndBody[1]);
            }
            else
            {
                this.HttpMessageBody = new HttpMessageBody(string.Empty);
            }

            if (this.HttpHeader.ContentLengthValueIsBlank())
            {
                this.HttpHeader.SetContentLength(this.HttpMessageBody.ByteLength);
            }

        }
        internal HttpStatusLine HttpStatusLine { get; }
        internal HttpHeader HttpHeader { get; }
        internal HttpMessageBody HttpMessageBody { get; }
        internal byte[] ToBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString());
        }
        public override string ToString()
        {
            string serializedData = this.HttpStatusLine.ToString() + "\r\n" + this.HttpHeader.ToString() + "\r\n\r\n";
            if (this.HttpMessageBody.ByteLength > 0)
            {
                serializedData += this.HttpMessageBody.ToString();
            }

            return serializedData;
        }
    }
}
