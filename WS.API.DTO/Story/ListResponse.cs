using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.API.DTOs.Story
{
    public class ListResponse<T>  where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<T> Results { get; set; }
    }
}