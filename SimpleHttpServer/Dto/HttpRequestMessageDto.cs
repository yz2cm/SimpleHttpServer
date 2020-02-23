using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Dto
{
    class HttpRequestMessageDto
    {
        internal HttpRequestMessageDto(HttpRequestLineDto requestLineDto, IReadOnlyList<HttpHeaderFieldEntryDto> headerFieldDtos, string httpMessageBody)
        {
            this.HttpRequestLine = requestLineDto;
            this.HttpHeaderFields = headerFieldDtos;
            this.HttpMessageBody = httpMessageBody;
        }
        internal static HttpRequestMessageDto Parse(string httpRequestMessage)
        {
            var headerAndBodySections = httpRequestMessage.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None);
            var headerSection = headerAndBodySections[0];
            var messageBody = headerAndBodySections.Length >= 2 ? headerAndBodySections[1] : string.Empty;
            var headerLines = headerSection.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            var requestLine = headerLines[0];
            IReadOnlyList<string> headerFieldLines = headerLines.Skip(1).ToList();

            var requestLineDto = HttpRequestLineDto.Parse(requestLine);
            IReadOnlyList<HttpHeaderFieldEntryDto> headerFieldEntryDtos = headerFieldLines.Select(x => HttpHeaderFieldEntryDto.Parse(x)).ToList();

            return new HttpRequestMessageDto(requestLineDto, headerFieldEntryDtos, messageBody);
        }
        internal HttpRequestLineDto HttpRequestLine { get; }
        internal IReadOnlyList<HttpHeaderFieldEntryDto> HttpHeaderFields { get; }
        internal string HttpMessageBody { get; }
        /// <summary>
        /// Structured http request line.
        /// ex) GET /index.html HTTP/1.1
        /// </summary>
        internal class HttpRequestLineDto
        {
            internal HttpRequestLineDto(string httpMethod, RequestedPathDto requestedPathDto, string httpVersion)
            {
                this.HttpMethod = httpMethod;
                this.RequestedPath = requestedPathDto;
                this.HttpVersion = httpVersion;
            }
            static internal HttpRequestLineDto Parse(string requestLine)
            {
                var parsed = requestLine.Split(' ');
                if (parsed.Length != 3)
                {
                    throw new ArgumentException($"Parse error. ({requestLine})");
                }
                string httpMethod = parsed[0];
                string requestedPathWithQuery = parsed[1];
                var requestedPathDto = RequestedPathDto.Parse(requestedPathWithQuery);
                string httpVersion = parsed[2];

                return new HttpRequestLineDto(parsed[0], requestedPathDto, parsed[2]);
            }
            internal string HttpMethod { get; }
            internal RequestedPathDto RequestedPath { get; }

            internal string HttpVersion { get; }
            internal class RequestedPathDto
            {
                static internal RequestedPathDto Parse(string path)
                {
                    var pathAndQuerySection = path.Split('?');
                    if (pathAndQuerySection.Length < 2)
                    {
                        return new RequestedPathDto(path, null);
                    }
                    else
                    {
                        string pathWithoutQuery = pathAndQuerySection[0];
                        string queryStringSection = pathAndQuerySection.Length >= 2 ? pathAndQuerySection[1] : string.Empty;

                        var kvs = QueryKeyValuePairCollectionDto.Parse(queryStringSection);

                        return new RequestedPathDto(pathWithoutQuery, kvs);
                    }
                }
                internal RequestedPathDto(string path, QueryKeyValuePairCollectionDto kvs)
                {
                    this.Path = path;
                    this.QueryKeyValueCollection = kvs;
                }
                internal string Path { get; }
                internal QueryKeyValuePairCollectionDto QueryKeyValueCollection { get; }
            }
        }
    }
}