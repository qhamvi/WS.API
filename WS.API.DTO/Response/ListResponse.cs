using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.API.DTO.Response
{
    public class ListResponse<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public List<T> Results { get; set; }
    }
}