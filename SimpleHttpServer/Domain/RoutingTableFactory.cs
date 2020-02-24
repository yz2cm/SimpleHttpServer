using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class RoutingTableFactory : IRoutingTableFactory
    {
        public RoutingTable Create(FilePath routingTableFileName)
        {
            var routingFile = RoutingFileDto.Load(routingTableFileName);
            var routePrefix = new RoutePrefix(routingFile.RoutePrefix);
            var routingEntries = new List<RoutingEntry>();
            foreach(var entry in routingFile.Routes)
            {
                var routePath = new RoutePath(entry.Path);
                var routeFullPath = new RouteFullPath(routePrefix, routePath);
                var httpResponseFileDto = HttpResponseFileDto.Load(entry.HttpResponseFileName);
                var httpStatusLine = new HttpStatusLine(httpResponseFileDto.HttpStatusLine);
                var httpHeaderFieldEntries = httpResponseFileDto.HttpHeaderFieldEntries
                    .Select(x => new HttpHeaderFieldEntry(x.HederFieldName, x.HeaderFieldValue)).ToList();
                var httpHeaderFieldEntryCollection = new HttpHeaderFieldEntryCollection(httpHeaderFieldEntries);
                var httpMessageBody = new HttpMessageBody(httpResponseFileDto.HttpMessageBody);

                var httpResponse = new HttpResponse(httpStatusLine, httpHeaderFieldEntryCollection, httpMessageBody);
                var routingEntry = new RoutingEntry(routeFullPath, httpResponse, new FilePath(entry.HttpResponseFileName));
                routingEntries.Add(routingEntry);
            }
            var routingEntryCollection = new RoutingEntryCollection(routePrefix, routingEntries);

            return new RoutingTable(routingEntryCollection);
        }
    }
}
