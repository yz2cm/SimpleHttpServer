using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class ProcessArgument
    {
        static internal (bool, string) TestValidation(string[] args)
        {
            if (args.Length != 2)
            {
                return (false, "Invalid argument");
            }
            if (args[0] != "--port")
            {
                return (false, "Invalid argument");
            }
            if (!int.TryParse(args[1], out _))
            {
                return (false, "Invalid port number is specified");
            }

            return (true, string.Empty);
        }
    }
}
