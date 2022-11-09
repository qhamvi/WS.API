using System;
using System.Collections.Generic;
using WS.API.Models;

namespace WS.API.Service
{
    public interface IChapterService
    {

        Chapter GetChapter(Guid idChapter);
        IEnumerable<Chapter> GetChapters();

        void CreateChapter(Chapter chapter);
        void UpdateChapter(Chapter chapter);
        void DeleteChapter(Guid idChapter);
    }
}
