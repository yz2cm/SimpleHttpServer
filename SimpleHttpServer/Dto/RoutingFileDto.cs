using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using SimpleHttpServer.Domain;

namespace SimpleHttpServer.Dto
{
    class RoutingFileDto
    {
        internal RoutingFileDto(string routePrefix, IReadOnlyList<RouteDto> routes)
        {
            this.RoutePrefix = routePrefix;
            this.Routes = routes;
        }
        static internal RoutingFileDto Load(FilePath routingFileName)
        {
            if(!routingFileName.Exists())
            {
                throw new FileNotFoundException(routingFileName.FullPath);
            }

            var routing = XDocument.Load(routingFileName.FullPath);
            var routingMap = routing.Element("RoutingMap");
            var routePrefix = routingMap.Element("RoutePrefix").Value;

            var routes = new List<RouteDto>();

            foreach (var entry in routingMap.Element("Routes").Elements("Route"))
            {
                var path = entry.Attribute("Path").Value;
                var httpResponseFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entry.Value);

                routes.Add(new RouteDto(path, httpResponseFileName));
            }

            return new RoutingFileDto(routePrefix, routes);
        }
        internal string RoutePrefix { get; }
        internal IReadOnlyList<RouteDto> Routes { get; private protected set; }
        internal class RouteDto
        {
            internal RouteDto(string path, string httpResponseFileName)
            {
                this.Path = path;
                this.HttpResponseFileName = httpResponseFileName;
            }
            internal string Path { get; }
            internal string HttpResponseFileName { get; }
        }
    }
}
