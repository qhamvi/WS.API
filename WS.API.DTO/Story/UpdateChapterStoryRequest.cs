using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.API.DTO.Story
{
    public class UpdateChapterStoryRequest
    {
        public Guid IdStory { get; set; }
        public List<string> ListChap { get; set; }

    }
}
