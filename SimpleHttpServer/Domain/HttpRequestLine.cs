using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpRequestLine
    {
        internal HttpRequestLine(string httpMethod, string requestedUri, string httpVersion, QueryKeyValueCollection queryKeyValueCollection)
        {
            this.HttpMethod = httpMethod;
            this.RequestedUri = requestedUri;
            this.queryKeyValueCollection = queryKeyValueCollection;
        }
        internal string HttpMethod { get; }
        internal string RequestedUri { get; }
        private QueryKeyValueCollection queryKeyValueCollection;
        internal string HttpVersion { get; }
        public override string ToString()
        {
            string requestLine = this.HttpMethod + " " + this.RequestedUri + " " + this.HttpVersion;

            return requestLine;
        }
        internal class QueryKeyValueCollection
        {
            internal QueryKeyValueCollection(IReadOnlyList<QueryKeyValue> queryKeyValues)
            {
                this.QueryKeyValues = queryKeyValues;
            }
            internal IReadOnlyList<QueryKeyValue> QueryKeyValues { get; }
        }
        internal class QueryKeyValue
        {
            internal QueryKeyValue(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
            private string key;
            private string value;
        }
    }
}
