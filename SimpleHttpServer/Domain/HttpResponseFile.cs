using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHttpServer.Domain
{
    class HttpResponseFile
    {
        internal HttpResponseFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            this.fileName = fileName;
        }
        internal string GetFileContent()
        {
            using (var reader = new StreamReader(this.fileName, Encoding.UTF8))
            {
                string content = reader.ReadToEnd();

                return content;
            }

        }
        private string fileName;
    }
}
