using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Dto
{
    internal class HttpHeaderFieldEntryDto
    {
        static internal HttpHeaderFieldEntryDto Parse(string headerFieldLine)
        {
            var kv = headerFieldLine.Split(':');
            if (kv.Length < 2)
            {
                return new HttpHeaderFieldEntryDto(string.Empty, string.Empty);
            }
            var name = kv[0].Trim();
            var value = kv[1].Trim();

            return new HttpHeaderFieldEntryDto(name, value);
        }
        internal HttpHeaderFieldEntryDto(string name, string value)
        {
            this.HederFieldName = name;
            this.HeaderFieldValue = value;
        }
        internal string HederFieldName { get; }
        internal string HeaderFieldValue { get; }
    }
}
