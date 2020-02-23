using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using SimpleHttpServer.Domain;
using SimpleHttpServer.Dto;

namespace SimpleHttpServer.Application
{
    class HttpServer
    {
        internal HttpServer(int port, RoutingTable routingTable, LoggerBase logger)
        {
            this.port = port;
            this.routingTable = routingTable;
            this.logger = logger;
        }
        public void Shutdown()
        {

        }
        public void Start()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Any, this.port);
            }
            catch(SocketException e)
            {
                this.logger.WriteError(e.Message + " (socket error code = " + e.SocketErrorCode.ToString() + ")");
            }
            listener.Start();

            while (true)
            {
                this.logger.WriteInformation($"TCP : listening ... (port {this.port})");

                using (var client = listener.AcceptTcpClient())
                using (var stream = client.GetStream())
                {
                    this.logger.WriteInformation($"TCP : Tcp connection established from {client.Client.RemoteEndPoint}.");

                    string httpRequest = string.Empty;

                    while (client.Connected && stream.CanRead)
                    {
                        byte[] bufferRead = new Byte[4096];
                        int readSize = stream.Read(bufferRead, 0, bufferRead.Length);

                        this.logger.WriteInformation($"TCP : Read {readSize} bytes from Tcp stream.");

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
                        this.logger.WriteWarning("TCP : Read Empty bytes from Tcp stream.");

                        client.Close();
                        continue;
                    }

                    this.logger.WriteInformation("HTTP : Http Request received.");
                    this.logger.WriteDebug(httpRequest);

                    var httpRequestDto = HttpRequestMessageDto.Parse(httpRequest);
                    var request = new HttpRequest(httpRequestDto);
                    var routeFullPath = new RouteFullPath(new RoutePath(request.HttpRequestLine.RequestedUri));
                    var httpResponse = this.routingTable.Find(routeFullPath);

                    this.logger.WriteInformation("HTTP : Http Response sent.");
                    this.logger.WriteDebug(httpResponse.ToString());

                    var resopnseBytes = httpResponse.ToBytes();
                    stream.Write(resopnseBytes, 0, resopnseBytes.Length);

                    this.logger.WriteInformation($"TCP : Write {resopnseBytes.Length} bytes to Tcp stream.");
                }
            }
        }
        private int port;
        private RoutingTable routingTable;
        private LoggerBase logger;
    }
}
