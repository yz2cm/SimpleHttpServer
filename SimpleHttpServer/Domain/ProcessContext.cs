using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class ProcessContext
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
}
