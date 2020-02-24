using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class RoutingEntryCollection
    {
        internal RoutingEntryCollection(RoutePrefix routePrefix, IReadOnlyList<RoutingEntry> routingEntries)
        {
            this.prefix = routePrefix;
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
