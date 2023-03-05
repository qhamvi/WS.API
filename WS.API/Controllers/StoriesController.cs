using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WS.API.DTO.Story;
using WS.API.Extensions;
using WS.API.Service;
using WS.API.Service.Implements;

namespace WS.API.Controllers
{
    [ApiController]
    [Route("stories")]
    public class StoriesController : ControllerBase
    {
        private readonly  IStoryService _service;
        
        public StoriesController(IStoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lấy danh sách story
        /// </summary>
        /// <param name="request"></param>    
        [HttpGet]
        public async Task<GetListStoryResponse> GetListAsync([FromQuery]GetListStoryRequest request)
        {
            var listStoryResponse = await _service.GetStoriesAsync(request);
            return listStoryResponse;
        }

        /// <summary>
        /// Tạo 1 story
        /// </summary>
        /// <param name="request"></param>    
        [HttpPost]
        public async Task<ActionResult> CreateAsync(StoryRequest request)
        {
            await _service.CreateStoryAsync(request);
            return Ok("Create Story Successs");
        }

        /// <summary>
        /// Lấy thông tin của 1 story
        /// </summary>
        /// <param name="idStory"></param> 
        [HttpGet("{idStory}")]
        public async Task<ActionResult<StoryResponse>> GetStoryAsync(Guid idStory)
        {
            var existStory = await _service.GetStoryAsync(idStory);
            if (existStory == null)
            {
                return NotFound("Story does not exist. ");
            }
            return Ok(existStory.AsStoryDto());
        }

        /// <summary>
        /// Cập nhật trạng thái của 1 story
        /// </summary>
        /// <param name="idStory"></param> 
        [HttpPut("{idStory}")]
        public async Task<ActionResult<StoryResponse>> UpdateStatusAsync(Guid idStory)
        {
            var existStory = await _service.GetStoryAsync(idStory);
            if(existStory == null)
            {
                return NotFound("Story does not exist. ");
            }
            var story = await _service.UpdateStatusStoryAsync(idStory);
            return Ok(story);
        }

        /// <summary>
        /// Cập nhật 1 story
        /// </summary>
        /// <param name="request"></param> 
        [HttpPut]
        public async Task<ActionResult<StoryResponse>> UpdateStoryAsync([FromBody] UpdateStoryRequest request)
        {
            var existStory = await _service.GetStoryAsync(request.IdStory);
            if (existStory == null)
            {
                return NotFound("Story does not exist. ");
            }
            var story = await _service.UpdateStoryAsync(request);
            return Ok(story);
        }

        /// <summary>
        /// Delete 1 story
        /// </summary>
        /// <param name="idStory"></param> 
        [HttpDelete("idStory")]
        public async Task<ActionResult> DeleteStoryAsync(Guid idStory)
        {
            var existStory = await _service.GetStoryAsync(idStory);
            if (existStory == null)
            {
                return NotFound("Story does not exist. ");
            }
            await _service.DeleteStoryAsync(idStory);
            return Content("Delete success");
        }

        /// <summary>
        /// Cập nhật chapter cho 1 story
        /// </summary>
        /// <param name="idStory"></param>
        /// <param name="request"></param> 
        [HttpPut("chap/{idStory}")]
        public async Task<ActionResult<StoryResponse>> UpdateChapterStoryAsync(Guid idStory, UpdateChapterStoryRequest request)
        {
            request.IdStory = idStory;
            var existStory = await _service.GetStoryAsync(idStory);
            if (existStory == null)
            {
                return NotFound("Story does not exist. ");
            }
            var story = await _service.UpdateChapterStoryAsync(request);
            return Ok(story);
        }

        /// <summary>
        /// Xóa chapter cho 1 story
        /// </summary>
        /// <param name="idStory"></param>
        /// <param name="request"></param> 
        [HttpDelete("chap/{idStory}")]
        public async Task<ActionResult<StoryResponse>> DeleteChapterStoryAsync(Guid idStory, UpdateChapterStoryRequest request)
        {
            request.IdStory = idStory;
            var existStory = await _service.GetStoryAsync(idStory);
            if (existStory == null)
            {
                return NotFound("Story does not exist. ");
            }
            var story = await _service.DeleteChapterStoryAsync(request);
            return Ok(story);
        }

    }
}