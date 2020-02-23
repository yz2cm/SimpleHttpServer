using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Xml.Linq;
using SimpleHttpServer.Application;
using SimpleHttpServer.Domain;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerBase logger = new ConsoleLogger();
            {
                var result = ProcessArgument.Validate(args);
                if (!result.ok)
                {
                    logger.WriteInformation(result.message);
                    Console.WriteLine(new Usage());

                    return;
                }
            }

            var context = new ProcessContext(args);
            if (! context.RoutingFileName.Exists())
            {
                logger.WriteError($"{context.RoutingFileName.NameOnly} not found.\r\n({context.RoutingFileName})");
                return;
            }

            var routingFile = RoutingFileDto.Load(context.RoutingFileName);
            var routingTable = new RoutingTable(routingFile);

            var httpServer = new HttpServer(context.PortNo, routingTable, logger);
            httpServer.Start();

        }
    }
}
