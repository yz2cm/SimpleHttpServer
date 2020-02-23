using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class HttpRequestLine
    {
        internal HttpRequestLine(HttpRequestMessageDto.HttpRequestLineDto requestLineDto)
        {
            this.HttpMethod = requestLineDto.HttpMethod;
            this.RequestedUri = requestLineDto.RequestedPath.Path;
            this.HttpVersion = requestLineDto.HttpVersion;
            this.QueryKeyValuePairs = new QueryKeyValueCollection(requestLineDto.RequestedPath.QueryKeyValueCollection);
        }
        internal string HttpMethod { get; }
        internal string RequestedUri { get; }
        internal QueryKeyValueCollection QueryKeyValuePairs { get; }
        internal string HttpVersion { get; }
        public override string ToString()
        {
            string requestLine = this.HttpMethod + " " + this.RequestedUri + " " + this.HttpVersion;

            return requestLine;
        }
        internal class QueryKeyValueCollection
        {
            internal QueryKeyValueCollection(QueryKeyValuePairCollectionDto kvCollection)
            {
                this.QueryKeyValues = kvCollection?.QueryKeyValuePairs.Select(x => new QueryKeyValue(x.Key, x.Value)).ToList();
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
