using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class HttpHeaderFieldEntryCollection
    {
        internal HttpHeaderFieldEntryCollection(IReadOnlyList<HttpHeaderFieldEntryDto> headerFieldEntryDtos)
        {
            this.headerFieldEntries = headerFieldEntryDtos.Select(x => new HttpHeaderFieldEntry(x)).ToList();
        }
        internal bool ContentLengthValueIsBlank()
        {
            var contentLengthEntry = this.headerFieldEntries.FirstOrDefault(x => x.IsContentLength());
            return contentLengthEntry == null || contentLengthEntry.FieldValue.Trim().Length == 0;
        }
        internal void SetContentLength(int contentLength)
        {
            var entry = this.headerFieldEntries.FirstOrDefault(x => x.IsContentLength());
            if(entry == null)
            {
                this.headerFieldEntries.Add(HttpHeaderFieldEntry.BuildContentLength(contentLength));
            }
            else
            {
                var i = this.headerFieldEntries.FindIndex(x => x.IsContentLength());
                this.headerFieldEntries.RemoveAt(i);
                this.headerFieldEntries.Insert(i, HttpHeaderFieldEntry.BuildContentLength(contentLength));
            }
        }
        public override string ToString()
        {
            return string.Join("\r\n", this.headerFieldEntries.Select(entry => entry.ToString()));
        }
        private List<HttpHeaderFieldEntry> headerFieldEntries = new List<HttpHeaderFieldEntry>();
    }
}
