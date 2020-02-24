using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Domain
{
    class RoutingTable
    {
        internal RoutingTable(RoutingFileDto routingFileDto)
        {
            this.routingEntryCollection = new RoutingEntryCollection(routingFileDto);
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
