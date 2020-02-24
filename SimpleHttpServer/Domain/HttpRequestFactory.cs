using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class HttpRequestFactory : IHttpRequestFactory
    {
        public HttpRequest Create(string httpRequestMessage)
        {
            var httpRequestMessageDto = HttpRequestMessageDto.Parse(httpRequestMessage);

            var httpRequestLineDto = httpRequestMessageDto.HttpRequestLine;
            var queryKeyValuePairs = httpRequestLineDto.RequestedPath.QueryKeyValueCollection?.QueryKeyValuePairs.Select(x => new HttpRequestLine.QueryKeyValue(x.Key, x.Value)).ToList();
            var queryKeyValueCollection = new HttpRequestLine.QueryKeyValueCollection(queryKeyValuePairs);

            var httpRequestLine = new HttpRequestLine(httpRequestLineDto.HttpMethod, httpRequestLineDto.RequestedPath.Path, httpRequestLineDto.HttpVersion, queryKeyValueCollection);            
            var httpHeaderFieldEntries = httpRequestMessageDto.HttpHeaderFields
                .Select(x => new HttpHeaderFieldEntry(x.HederFieldName, x.HeaderFieldValue)).ToList();

            var httpHeaderFieldEntryCollection = new HttpHeaderFieldEntryCollection(httpHeaderFieldEntries);
            var httpMessageBody = new HttpMessageBody(httpRequestMessageDto.HttpMessageBody);

            return new HttpRequest(httpRequestLine, httpHeaderFieldEntryCollection, httpMessageBody);
        }
    }
}
