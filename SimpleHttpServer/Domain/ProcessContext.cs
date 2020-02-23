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
            this.RoutingFileName = new FilePath(AppDomain.CurrentDomain.BaseDirectory, "routing.xml");
        }
        internal Int32 PortNo { get; }
        internal FilePath RoutingFileName { get; }
        internal string CurrentDirectoryName => AppDomain.CurrentDomain.BaseDirectory;
    }
}
