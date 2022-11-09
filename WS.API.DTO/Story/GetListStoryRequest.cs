using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.API.DTOs.Story
{
    public class GetListStoryRequest
    {
        public bool? StatusStory { get; set; }

        public bool? IsComplete {get; set;}

        public int Page { get; set; }

        public int PageSize { get; set; }

        public bool? IsAscSorting { get; set; }

        public string? Sort {get; set;}

        public string Search { get; set; }
    }
}