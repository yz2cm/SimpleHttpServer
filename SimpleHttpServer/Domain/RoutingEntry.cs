using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class RoutingEntry
    {
        internal RoutingEntry(RouteFullPath path, HttpResponse response, FilePath responseFileName)
        {
            this.Path = path;
            this.Response = response;
            this.ResponseFileName = responseFileName;
        }
        internal RouteFullPath Path { get; }
        internal HttpResponse Response { get; }
        internal FilePath ResponseFileName { get; }
    }
}
