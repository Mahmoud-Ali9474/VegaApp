using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Core.Models
{
    public class QueryResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public IEnumerable<T> PagedList { get; set; }
        public QueryResult()
        {
            PagedList = new List<T>();
        }
    }
}