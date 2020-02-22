using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class RoutingEntry
    {
        internal RoutingEntry(RouteFullPath path, HttpResponse response)
        {
            this.Path = path;
            this.Response = response;
        }
        internal RouteFullPath Path { get; }
        internal HttpResponse Response { get; }
    }
}
