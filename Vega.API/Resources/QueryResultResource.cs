using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vega.API.Resources
{
    public class QueryResultResource<T> where T:class
    {
       
        public int TotalCount { get; set; }
        public IEnumerable<T> PagedList { get; set; }
        public QueryResultResource()
        {
            PagedList = new List<T>();
        } 
    }
}