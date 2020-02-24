﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    interface IHttpRequestFactory
    {
        HttpRequest Create(string httpRequestMessage);
    }
}
