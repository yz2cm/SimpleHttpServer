using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class HttpMessageBody
    {
        internal HttpMessageBody(string messageBody)
        {
            this.messageBody = messageBody;
        }
        public override string ToString()
        {
            return this.messageBody;
        }
        internal int ByteLength => Encoding.UTF8.GetBytes(this.messageBody).Length;
        private string messageBody;
    }
}
