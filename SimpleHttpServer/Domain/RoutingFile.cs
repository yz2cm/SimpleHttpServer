using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class RoutingFile
    {
        internal RoutingFile(string routingFileName)
        {
            this.routingFileName = routingFileName;

            var routing = XDocument.Load(routingFileName);
            var routingMap = routing.Element("RoutingMap");
            var routePrefix = routingMap.Element("RoutePrefix").Value;

            var routes = new List<RoutingMap.Route>();

            foreach (var entry in routingMap.Elements("Route"))
            {
                var path = entry.Attribute("Path").Value;
                var responseFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entry.Value);

                routes.Add(new RoutingMap.Route(path, responseFile));
            }
            this.map = new RoutingMap(routePrefix, routes);
        }
        private string routingFileName;
        internal bool Exists()
        {
            return File.Exists(this.routingFileName);
        }
        internal RoutingMap GetRoutingMap() {
            return this.map;
        }
        internal (bool, string) TestValidation()
        {
            var noExistFile = this.map.Routes.FirstOrDefault(x => !x.ResponseFileExists());
            if(noExistFile == null)
            {
                return (true, string.Empty);
            }
            else
            {
                return (false, "Response file not found (" + noExistFile.ResponseFileName + ")");
            }
        }
        private RoutingMap map;
        internal class RoutingMap
        {
            internal RoutingMap(string routePrefix, IReadOnlyList<Route> routes)
            {
                this.RoutePrefix = new RoutePrefix(routePrefix);
                this.Routes = routes;
            }
            internal RoutePrefix RoutePrefix;
            internal IReadOnlyList<Route> Routes;
            internal class Route
            {
                internal Route(string path, string responseFileName)
                {
                    this.Path = path;
                    this.ResponseFileName = responseFileName;
                }
                internal bool ResponseFileExists()
                {
                    return File.Exists(this.ResponseFileName);
                }
                internal string Path { get; }
                internal string ResponseFileName { get; }
            }
        }
    }
}
