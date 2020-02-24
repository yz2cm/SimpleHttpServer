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
                var routeFullPath = new RouteFullPath(this.prefix, routePath);
                var httpResponseDto = HttpResponseFileDto.Load(entry.HttpResponseFileName);
                var httpResponse = new HttpResponse(httpResponseDto);

                routingEntries.Add(new RoutingEntry(routeFullPath, httpResponse, new FilePath(entry.HttpResponseFileName)));
            }
            this.routingEntries = routingEntries;
        }
        internal RoutingEntry Find(RouteFullPath routeFullPath)
        {
            var matchedEntry = this.routingEntries.Where(entry => entry.Path.Equals(routeFullPath)).FirstOrDefault();
            
            return matchedEntry;
        }
        internal RoutingEntry DefaultEntry
        {
            get
            {
                var defaultEntry = this.routingEntries.Where(entry => entry.Path.Equals(RouteFullPath.Default(this.prefix))).FirstOrDefault();

                return defaultEntry;
            }
        }
        private IReadOnlyList<RoutingEntry> routingEntries;
        private RoutePrefix prefix;
    }
}
