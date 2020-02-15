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

namespace SimpleHttpServer
{
    class Program
    {
        static bool tryParseArguments(string[] args)
        {
            if (args.Length != 2) return false;
            if (args[0] != "--port") return false;
            Int32 port;
            if (!int.TryParse(args[1], out port)) return false;

            return true;
        }
        internal class ProcessContext
        {
            internal ProcessContext(string[] args)
            {
                Int32 port;
                int.TryParse(args[1], out port);
                this.PortNo = port;
            }
            internal Int32 PortNo { get; }
            internal string RoutingFileName => Path.Combine(this.CurrentDirectoryName, "routing.xml");
            internal string CurrentDirectoryName => AppDomain.CurrentDomain.BaseDirectory;
        }
        static void printUsage(string progName)
        {
            Console.WriteLine($"{progName} --port <port>");
        }
        static void startHttpServer(ProcessContext context, RoutingTable routingTable)
        {
            var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), context.PortNo);
            listener.Start();

            while (true)
            {
                Console.WriteLine($"listening ... (port {context.PortNo})");
                using (var client = listener.AcceptTcpClient())
                using (var stream = client.GetStream())
                {
                    Console.WriteLine();
                    Console.WriteLine($"**** Accept from {client.Client.RemoteEndPoint} ***");
                    Console.WriteLine();

                    string httpRequest = string.Empty;

                    while (client.Connected && stream.CanRead)
                    {
                        byte[] bufferRead = new Byte[4096];
                        int readSize = stream.Read(bufferRead, 0, bufferRead.Length);

                        if(readSize == 0)
                        {
                            break;
                        }

                        httpRequest += Encoding.UTF8.GetString(bufferRead, 0, readSize);

                        if(readSize < bufferRead.Length)
                        {
                            break;
                        }
                    }

                    if(httpRequest.Length == 0)
                    {
                        continue;
                    }

                    Console.WriteLine(">>>> Http Request :");
                    Console.WriteLine(httpRequest);
                    Console.WriteLine();

                    var httpStartLine = httpRequest.Split(new string[] { "\r\n" }, StringSplitOptions.None)[0];
                    var routingPath = httpStartLine.Split(' ')[1];
                    var httpResponse = routingTable.GetReponse(routingPath);
                    Console.WriteLine("<<<< Http Response :");
                    Console.WriteLine(httpResponse);
                    Console.WriteLine();

                    byte[] bytes = routingTable.GetResponseBytes(routingPath);
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

        }
        static void Main(string[] args)
        {
            var progName = Process.GetCurrentProcess().MainModule.FileName;

            if (! tryParseArguments(args))
            {
                printUsage(progName);
                return;
            }

            var context = new ProcessContext(args);

            if (! File.Exists(context.RoutingFileName))
            {
                Console.WriteLine($"error : Routing file is not found. ({context.RoutingFileName})");
                return;
            }

            var routingTable = new RoutingTable(context.RoutingFileName);

            foreach(var x in routingTable.GetNonExistResponseFiles())
            {
                Console.WriteLine($"error : Response file is not found. ({x})");
                return;
            }

            startHttpServer(context, routingTable);
        }
    }
}
