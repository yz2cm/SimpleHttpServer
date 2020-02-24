using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class RoutingTable
    {
        internal RoutingTable(RoutingEntryCollection routingEntryCollection)
        {
            this.routingEntryCollection = routingEntryCollection;
        }
        internal RoutingEntry FindOrDefault(RouteFullPath fullPath)
        {
            var matchedEntry = this.routingEntryCollection.Find(fullPath);
            if(matchedEntry != null)
            {
                return matchedEntry;
            }

            var defaultEntry = this.routingEntryCollection.DefaultEntry;
            if (defaultEntry != null)
            {
                return defaultEntry;
            }

            throw new KeyNotFoundException(fullPath.ToString());
        }
        private RoutingEntryCollection routingEntryCollection;
    }
}
