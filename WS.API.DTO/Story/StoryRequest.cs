using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.API.DTOs.Story
{
    public class StoryRequest
    {
        public string TitleStory { get; init; }

        public string Author { get; init; }

        public string Collector { get; init; }

        public string Summary { get; init; }

        public List<string> ListTopic { get; init; }
        
        public List<string> ListChap { get; init; }

        public bool Complete { get; init; }

        public string ImageFileName { get; init; }

    }
}