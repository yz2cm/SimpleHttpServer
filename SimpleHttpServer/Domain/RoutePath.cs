using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class RoutePath
    {
        internal RoutePath(string path)
        {
            this.path = path;
        }
        internal string Path => this.path;
        private string path;
    }
}
