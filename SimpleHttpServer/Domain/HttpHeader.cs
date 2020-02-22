using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpHeader
    {
        internal HttpHeader(string headerFieldSection) : this(headerFieldSection.Split(new string[] { "\r\n" }, StringSplitOptions.None))
        {
        }
        internal HttpHeader(IReadOnlyList<string> headerFieldLines)
        {
            foreach(var headerLine in headerFieldLines)
            {
                var entry = new HttpHeaderEntry(headerLine);

                this.headerEntries.Add(entry);
            }
        }
        internal bool ContentLengthValueIsBlank()
        {
            var entry = this.headerEntries.FirstOrDefault(x => x.IsContentLength());
            return entry == null || entry.FieldValue.Trim().Length == 0;
        }
        internal void SetContentLength(int contentLength)
        {
            var entry = this.headerEntries.FirstOrDefault(x => x.IsContentLength());
            if(entry == null)
            {
                this.headerEntries.Add(HttpHeaderEntry.BuildContentLength(contentLength));
            }
            else
            {
                var i = this.headerEntries.FindIndex(x => x.IsContentLength());
                this.headerEntries.RemoveAt(i);
                this.headerEntries.Insert(i, HttpHeaderEntry.BuildContentLength(contentLength));
            }
        }
        public override string ToString()
        {
            return string.Join("\r\n", this.headerEntries.Select(entry => entry.ToString()));
        }
        private List<HttpHeaderEntry> headerEntries = new List<HttpHeaderEntry>();
    }
}
