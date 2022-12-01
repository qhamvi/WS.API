using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.API.DTO.Story
{
    public class UpdateStoryRequest
    {
        public Guid IdStory { get; init; }

        public string TitleStory { get; init; }

        public string Author { get; init; }

        public string Collector { get; init; }

        public string Summary { get; init; }

        public bool Complete { get; init; }

        public string ImageFileName { get; init; }
    }
}
