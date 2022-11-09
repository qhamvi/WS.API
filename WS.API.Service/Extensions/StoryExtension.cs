using System;
using System.Collections.Generic;
using WS.API.DTOs.Story;

namespace WS.API.Extensions
{
    public static class StoryExtension
    {
         public static Dictionary<string, Func<StoryResponse, object>> KeySortParameters = new()
        {
            {"author", x => x.Author},
            {"collector", x => x.Collector},
            {"publishDate", x => x.PublishDate},
            {"createDate", x => x.CreateDate},
            {"titleStory", x => x.TitleStory},
            {"numberChap", x => x.NumberChap },
            
        };
    }
}