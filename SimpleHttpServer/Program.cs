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
using SimpleHttpServer.Domain;

namespace SimpleHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                (bool ok, string message) = ProcessArgument.TestValidation(args);
                if (!ok)
                {
                    Console.WriteLine(message);
                    Console.WriteLine(new Usage());

                    return;
                }
            }

            var context = new ProcessContext(args);
            if (! File.Exists(context.RoutingFileName))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\r\n[Error] routing.xml not found.");
                Console.ResetColor();
                Console.WriteLine("(" + context.RoutingFileName + ")");
                return;
            }

            var routingFile = new RoutingFile(context.RoutingFileName);

            {
                (bool ok, string message) = routingFile.TestValidation();
                if (!ok)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\r\n[Error] Routing file validation error.");
                    Console.ResetColor();
                    Console.WriteLine(message);
                    return;
                }
            }
            var routingTable = new RoutingTable(routingFile);

            var httpServer = new HttpServer(context.PortNo, routingTable);
            httpServer.Start();

        }
    }
}
