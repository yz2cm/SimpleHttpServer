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
                Console.WriteLine($"[TCP] listening ... (port {this.port})");
                using (var client = listener.AcceptTcpClient())
                using (var stream = client.GetStream())
                {
                    Console.WriteLine($"[TCP] Tcp connection established from {client.Client.RemoteEndPoint}.");

                    string httpRequest = string.Empty;

                    while (client.Connected && stream.CanRead)
                    {
                        byte[] bufferRead = new Byte[4096];
                        int readSize = stream.Read(bufferRead, 0, bufferRead.Length);

                        Console.WriteLine($"[TCP] Read {readSize} bytes from Tcp stream.");

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
                        Console.WriteLine($"[TCP] Read Empty bytes from Tcp stream.");

                        client.Close();
                        continue;
                    }

                    Console.WriteLine("[HTTP] Http Request received.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(httpRequest);
                    Console.ResetColor();
                    Console.WriteLine();

                    var request = new HttpRequest(httpRequest);
                    var routeFullPath = new RouteFullPath(request.HttpRequestLine.RequestUri);
                    var httpResponse = this.routingTable.GetHtpResponse(routeFullPath);

                    Console.WriteLine("[HTTP] Http Response sent.");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(httpResponse);
                    Console.ResetColor();
                    Console.WriteLine();

                    var resopnseBytes = httpResponse.ToBytes();
                    stream.Write(resopnseBytes, 0, resopnseBytes.Length);

                    Console.WriteLine($"[TCP] Write {resopnseBytes.Length} bytes to Tcp stream.");
                }
            }
        }
        private int port;
        private RoutingTable routingTable;
    }
}
