﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.API.DTO.Topic
{
    public class GetListTopicRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public bool? IsAscSorting { get; set; }

        public string? Sort { get; set; }

    }
}
