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
        internal RoutingTable(RoutingFile routingFile)
        {
            var map = routingFile.GetRoutingMap();
            this.routePrefix = map.RoutePrefix;

            foreach(var route in map.Routes)
            {
                var routeFullPath = new RouteFullPath(map.RoutePrefix, new RoutePath(route.Path));
                var httpResponse = new HttpResponse(new HttpResponseFile(route.ResponseFileName));

                this.routeTable.Add(new RoutingEntry(routeFullPath, httpResponse));
            }
        }
        internal HttpResponse GetHtpResponse(RouteFullPath path)
        {
            var result = this.routeTable.FirstOrDefault(entry => entry.Path.ToString() == path.ToString());
            if (result == null)
            {
                result = this.routeTable.FirstOrDefault(entry => entry.Path.ToString() == RouteFullPath.Default(this.routePrefix).ToString());
                if (result == null)
                {
                    throw new KeyNotFoundException(path.ToString());
                }
            }

            return result.Response;
        }
        private RoutePrefix routePrefix;
        private List<RoutingEntry> routeTable = new List<RoutingEntry>();
    }
}
