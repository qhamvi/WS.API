﻿using System;
using System.Collections.Generic;

namespace WS.API.Models
{
    public record Story
    {
        public Guid Id { get; init; }

        public string TitleStory { get; init; }

        public string Author { get; init; }

        public string Collector { get; init; }
        public List<string> ListTopic { get; init; }
        public bool Complete { get; init; }
        public bool Status { get; init; }

        public DateTime CreateDate { get; init; }

        public DateTime? PublishDate { get; init; }
        public string ImageFileName { get; init; }

        public int NumberChap { get; init; }
        public List<string> ListChap { get; init; }
        public string Summary { get; init; }

        public List<string> IdCom { get; init; }

    }
}
