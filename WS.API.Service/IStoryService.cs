using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTOs.Story;
using WS.API.Models;

namespace WS.API.Service
{
    public interface IStoryService
    {
        Task CreateStoryAsync(StoryRequest story);
        Task DeleteStoryAsync(Guid idStory);
        Task<GetListStoryResponse> GetStoriesAsync(GetListStoryRequest request);
        Task<Story> GetStoryAsync(Guid idStory);
        Task<StoryResponse> UpdateStatusStoryAsync(Guid idStory);
        Task<StoryResponse> UpdateChapterStoryAsync(UpdateChapterStoryRequest request);
        Task<StoryResponse> DeleteChapterStoryAsync(UpdateChapterStoryRequest request);

        Task<StoryResponse> UpdateStoryAsync(UpdateStoryRequest story); 

    }
}
