using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class RoutingEntryCollection
    {
        internal RoutingEntryCollection(RoutingFileDto routingFileDto)
        {
            var routingEntries = new List<RoutingEntry>();
            this.prefix = new RoutePrefix(routingFileDto.RoutePrefix);

            foreach(var entry in routingFileDto.Routes)
            {
                var routePath = new RoutePath(entry.Path);
                var routeFullPath = new RouteFullPath(prefix, routePath);
                var httpResponseDto = HttpResponseFileDto.Load(entry.HttpResponseFileName);
                var httpResponse = new HttpResponse(httpResponseDto);

                routingEntries.Add(new RoutingEntry(routeFullPath, httpResponse, new FilePath(entry.HttpResponseFileName)));
            }
            this.routingEntries = routingEntries;
        }
        internal RoutingEntry Find(RouteFullPath routeFullPath)
        {
            var matchedEntry = this.routingEntries.Where(x => x.Path.Equals(routeFullPath)).FirstOrDefault();
            if(matchedEntry != null)
            {
                return matchedEntry;
            }

            var defaultEntry = this.routingEntries.Where(x => x.Path.Equals(RouteFullPath.Default(this.prefix))).FirstOrDefault();
            if(defaultEntry != null)
            {
                return defaultEntry;
            }

            throw new KeyNotFoundException(routeFullPath.ToString());
        }
        private IReadOnlyList<RoutingEntry> routingEntries;
        private RoutePrefix prefix;
    }
}
