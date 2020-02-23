using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class HttpHeaderFieldEntry
    {
        internal HttpHeaderFieldEntry(string key, string value)
        {
            this.FieldName = key;
            this.FieldValue = value;
        }
        internal HttpHeaderFieldEntry(HttpHeaderFieldEntryDto dto)
        {
            this.FieldName = dto.HederFieldName;
            this.FieldValue = dto.HeaderFieldValue;
        }
        private const string ContentLengthFieldName = "Content-Length";
        internal static HttpHeaderFieldEntry BuildContentLength(int length)
        {
            return new HttpHeaderFieldEntry(ContentLengthFieldName, length.ToString());
        }
        internal bool IsContentLength()
        {
            return String.Compare(this.FieldName, ContentLengthFieldName, true) == 0;
        }
        public override string ToString()
        {
            if(this.FieldName.Length == 0)
            {
                return string.Empty;
            }
            else
            {
                return this.FieldName + ": " + this.FieldValue;
            }
        }
        internal string FieldName { get; }
        internal string FieldValue { get; }
    }
}
