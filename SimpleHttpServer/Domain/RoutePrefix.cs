using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class RoutePrefix
    {
        internal RoutePrefix(string prefix)
        {
            this.Prefix = prefix;
        }
        internal string Prefix
        {
            get;
        }
    }
}
