using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTO.Chapter;
using WS.API.Models;

namespace WS.API.Service
{
    public interface IChapterService
    {

        Task<ChapterResponse> GetChapterAsync(Guid idChapter);
        Task<ListChapterResponse> GetChaptersAsync(ListChapterRequest request);
        Task<ChapterResponse> CreateChapterAsync(CreateChapterRequest request);
        Task<ChapterResponse> UpdateChapterAsync(ChapterRequest request);
        Task DeleteChapterAsync(Guid idChapter);
    }
}
