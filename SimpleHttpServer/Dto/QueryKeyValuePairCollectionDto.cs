using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHttpServer.Dto
{
    internal class QueryKeyValuePairCollectionDto
    {
        static internal QueryKeyValuePairCollectionDto Parse(string queryStringSection)
        {
            if(queryStringSection.Length == 0)
            {
                return null;
            }
            var kvSrings = queryStringSection.Split('&');
            var kvs = kvSrings.Select(x => QueryKeyValuePairDto.Parse(x)).ToList();

            return new QueryKeyValuePairCollectionDto(kvs);
        }
        internal QueryKeyValuePairCollectionDto(IReadOnlyList<QueryKeyValuePairDto> kvs)
        {
            this.QueryKeyValuePairs = kvs;
        }
        internal IReadOnlyList<QueryKeyValuePairDto> QueryKeyValuePairs { get; }
    }
}
