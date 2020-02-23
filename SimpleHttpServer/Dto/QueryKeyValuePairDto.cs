using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Dto
{
    internal class QueryKeyValuePairDto
    {
        static internal QueryKeyValuePairDto Parse(string kvString)
        {
            var kv = kvString.Split('=');
            string key = kv[0];
            string value = kv[1];

            return new QueryKeyValuePairDto(key, value);
        }
        internal QueryKeyValuePairDto(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        internal string Key { get; }
        internal string Value { get; }
    }
}
