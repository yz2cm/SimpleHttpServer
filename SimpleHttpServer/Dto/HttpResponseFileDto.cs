using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHttpServer.Dto
{
    class HttpResponseFileDto
    {
        internal HttpResponseFileDto(string httpStatusLine, IReadOnlyList<string> httpHeaders, string httpMessageBody)
        {
            this.HttpStatusLine = httpStatusLine;
            this.HttpHeaderFieldEntries = httpHeaders.Select(x => HttpHeaderFieldEntryDto.Parse(x)).ToList();
            this.HttpMessageBody = httpMessageBody;
        }
        static internal HttpResponseFileDto Load(string httpResponseFileName)
        {
            if (!File.Exists(httpResponseFileName))
            {
                throw new FileNotFoundException(httpResponseFileName);
            }
            {
                string content;
                using (var reader = new StreamReader(httpResponseFileName, Encoding.UTF8))
                {
                    content = reader.ReadToEnd();
                }
                var headerAndBody = content.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
                var headerSection = headerAndBody[0];
                string bodySection = headerAndBody.Length >= 2 ? headerAndBody[1] : string.Empty;

                var statusLineAndHeaderFieldsSection = headerSection.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                var statusLine = statusLineAndHeaderFieldsSection[0];
                IReadOnlyList<string> headerFields = statusLineAndHeaderFieldsSection.Skip(1).ToList();

                return new HttpResponseFileDto(statusLine, headerFields, bodySection);
            }
        }
        internal string HttpStatusLine { get; }
        internal IReadOnlyList<HttpHeaderFieldEntryDto> HttpHeaderFieldEntries { get; }
        internal string HttpMessageBody { get; }
    }
}
