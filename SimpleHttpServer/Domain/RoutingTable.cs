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
            this.routingFileDto = routingFileDto;
        }
        internal HttpResponse Find(RouteFullPath fullPath)
        {
            var table = routingFileDto.Routes;
            var prefix = new RoutePrefix(routingFileDto.RoutePrefix);

            var result = table.FirstOrDefault(entry => new RouteFullPath(prefix, new RoutePath(entry.Path)).ToString() == fullPath.ToString());
            if (result == null)
            {
                result = table.FirstOrDefault(entry => new RouteFullPath(new RoutePath(entry.Path)).Equals(RouteFullPath.Default()));
                if (result == null)
                {
                    throw new KeyNotFoundException(fullPath.ToString());
                }
            }

            var httpResponseFileDto =  HttpResponseFileDto.Load(result.HttpResponseFileName);
            return new HttpResponse(httpResponseFileDto);
        }
        private RoutingFileDto routingFileDto;
    }
}
