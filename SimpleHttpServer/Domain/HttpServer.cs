using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace SimpleHttpServer.Domain
{
    class HttpServer
    {
        internal HttpServer(int port, RoutingTable routingTable)
        {
            this.port = port;
            this.routingTable = routingTable;
        }
        public void Shutdown()
        {

        }
        public void Start()
        {
            var listener = new TcpListener(IPAddress.Any, this.port);
            listener.Start();

            while (true)
            {
                Console.WriteLine($"listening ... (port {this.port})");
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

                        if (readSize == 0)
                        {
                            break;
                        }

                        httpRequest += Encoding.UTF8.GetString(bufferRead, 0, readSize);

                        if (readSize < bufferRead.Length)
                        {
                            break;
                        }
                    }

                    if (httpRequest.Length == 0)
                    {
                        continue;
                    }

                    Console.WriteLine(">>>> Http Request :");
                    Console.WriteLine(httpRequest);
                    Console.WriteLine();

                    var request = new HttpRequest(httpRequest);
                    var routeFullPath = new RouteFullPath(request.HttpRequestLine.RequestUri);
                    var httpResponse = this.routingTable.GetHtpResponse(routeFullPath);

                    Console.WriteLine("<<<< Http Response :");
                    Console.WriteLine(httpResponse);
                    Console.WriteLine();

                    stream.Write(httpResponse.ToBytes(), 0, httpResponse.ToBytes().Length);
                }
            }
        }
        private int port;
        private RoutingTable routingTable;
    }
}
