using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class FilePath
    {
        internal FilePath(string path)
        {
            this.path = path;
        }
        internal FilePath(string directoryName, string nameOnly)
        {
            this.path = Path.Combine(directoryName, nameOnly);
        }
        internal bool Exists()
        {
            return File.Exists(this.path);
        }
        internal string NameOnly => Path.GetFileName(path);
        internal string FullPath => this.path;
        private string path;
    }
}
