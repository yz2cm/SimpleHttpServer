using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace SimpleHttpServer
{
    class RoutingTable
    {
        internal RoutingTable(string routingFileName)
        {
            var routing = XDocument.Load(routingFileName);
            var routingMap = routing.Element("routingMap");
            foreach (var entry in routingMap.Elements("route"))
            {
                var path = entry.Attribute("path").Value;
                var responseFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, entry.Value);

                this.table.Add(path, responseFile);
            }
        }
        internal byte[] GetResponseBytes(string path)
        {
            var response = this.GetReponse(path);

            return UTF8Encoding.UTF8.GetBytes(response);
        }
        internal string GetReponse(string path)
        {
            string responseFile;
            if (! this.table.TryGetValue(path, out responseFile))
            {
                if(!this.table.TryGetValue("*", out responseFile))
                {
                    throw new KeyNotFoundException(path);
                }
            }

            if(!File.Exists(responseFile))
            {
                throw new FileNotFoundException(responseFile);
            }

            using (var reader = new System.IO.StreamReader(responseFile, Encoding.UTF8))
            {
                string response = reader.ReadToEnd();
                return response;
            }
        }
        internal IReadOnlyList<string> GetNonExistResponseFiles()
        {
            return this.table.Values.Where(x => !File.Exists(x)).ToList();
        }
        private Dictionary<string, string> table = new Dictionary<string, string>();
    }
}
