using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpHeaderEntry
    {
        internal HttpHeaderEntry(string headerLine)
        {
            var kv = headerLine.Split(':');
            this.FieldName = kv[0].Trim();
            if(kv.Length >= 2)
            {
                this.FieldValue = kv[1].Trim();
            }
            else
            {
                this.FieldValue = string.Empty;
            }
        }
        internal HttpHeaderEntry(string fieldName, string fieldValue)
        {
            this.FieldName = fieldName;
            this.FieldValue = fieldValue;
        }
        private const string ContentLengthFieldName = "Content-Length";
        internal static HttpHeaderEntry BuildContentLength(int length)
        {
            return new HttpHeaderEntry(ContentLengthFieldName, length.ToString());
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
                return this.FieldName + " : " + this.FieldValue;
            }
        }
        internal string FieldName { get; }
        internal string FieldValue { get; }
    }
}
