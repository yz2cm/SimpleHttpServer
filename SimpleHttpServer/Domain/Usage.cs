using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class Usage
    {
        public override string ToString()
        {
            string progName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            string usage = $"{progName} --port <port>";

            return usage;
        }
    }
}
