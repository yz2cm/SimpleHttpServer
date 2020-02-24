using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Domain
{
    class RouteFullPath
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.ToString() != null ? this.GetHashCode() : 0) * 397;
            }
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return this.Equals((RouteFullPath)obj);
        }
        public bool Equals(RouteFullPath other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(this.ToString(), other.ToString());
        }
        internal RouteFullPath(RoutePath path)
        {
            this.prefix = new RoutePrefix(string.Empty);
            this.path = path;
        }
        internal RouteFullPath(RoutePrefix prefix, RoutePath path)
        {
            this.prefix = prefix;
            this.path = path;
        }
        public override string ToString()
        {
            string fullPath = string.Empty;
            if(this.prefix.Prefix.Length > 0)
            {
                if (this.prefix.Prefix.StartsWith("/"))
                {
                    fullPath += this.prefix.Prefix;
                }
                else
                {
                    fullPath += "/" + this.prefix.Prefix;
                }
            }
            if(this.path.Path.Length > 0)
            {
                if (fullPath.EndsWith("/") || this.path.Path.StartsWith("/"))
                {
                    fullPath += this.path.Path;
                }
                else
                {
                    fullPath += "/" + this.path.Path;
                }
            }

            return fullPath;
        }
        internal static RouteFullPath Default(RoutePrefix prefix) => new RouteFullPath(prefix, new RoutePath("*"));
        private RoutePrefix prefix;
        private RoutePath path;
    }
}
