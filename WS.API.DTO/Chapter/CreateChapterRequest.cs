using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.API.DTO.Chapter
{
    public class CreateChapterRequest
    {
        public string TitleChap { get; init; }

        public string IdStory { get; init; }

        public string Collector { get; init; }

        public string Content { get; init; }
    }
}
