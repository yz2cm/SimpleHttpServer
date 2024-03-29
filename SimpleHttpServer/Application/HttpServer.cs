﻿using System;
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
        internal HttpServer(int port, RoutingTable routingTable, LoggerBase logger, IHttpRequestFactory httpRequestFactory)
        {
            this.port = port;
            this.routingTable = routingTable;
            this.logger = logger;
            this.httpRequestFactory = httpRequestFactory;
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
                    this.logger.WriteNotification($"TCP : Tcp connection established. ({client.Client.RemoteEndPoint} ====> {client.Client.LocalEndPoint}");

                    string httpRequestMessage = string.Empty;

                    while (client.Connected && stream.CanRead)
                    {
                        byte[] bufferRead = new Byte[4096];
                        this.logger.WriteInformation($"TCP : Read from Tcp stream...");

                        int readSize = stream.Read(bufferRead, 0, bufferRead.Length);

                        this.logger.WriteInformation($"TCP : Read {readSize} bytes from Tcp stream.");

                        if (readSize == 0)
                        {
                            break;
                        }

                        httpRequestMessage += Encoding.UTF8.GetString(bufferRead, 0, readSize);

                        if (readSize < bufferRead.Length)
                        {
                            break;
                        }
                    }

                    if (httpRequestMessage.Length == 0)
                    {
                        this.logger.WriteWarning("TCP : Read Empty bytes from Tcp stream.");

                        client.Close();
                        continue;
                    }

                    this.logger.WriteInformation("HTTP : Http Request received.");
                    this.logger.WriteDebug(httpRequestMessage);

                    var httpRequest = httpRequestFactory.Create(httpRequestMessage);
                    var requestedPath = new RouteFullPath(new RoutePath(httpRequest.HttpRequestLine.RequestedUri));
                    var response = this.routingTable.FindOrDefault(requestedPath);

                    this.logger.WriteInformation("Routing completed.");
                    this.logger.WriteDebug($"  * Matched route  : {response.Path}");
                    this.logger.WriteDebug($"  * Response file  : {response.ResponseFileName.NameOnly}");

                    this.logger.WriteInformation("HTTP : Http Response sent.");
                    this.logger.WriteDebug(response.Response.ToString());

                    var resopnseBytes = response.Response.ToBytes();
                    stream.Write(resopnseBytes, 0, resopnseBytes.Length);

                    this.logger.WriteInformation($"TCP : Write {resopnseBytes.Length} bytes to Tcp stream.");
                    this.logger.WriteNotification($"HTTP : Session sucessfully completed.");
                }
            }
        }
        private int port;
        private RoutingTable routingTable;
        private LoggerBase logger;
        private IHttpRequestFactory httpRequestFactory;
    }
}
