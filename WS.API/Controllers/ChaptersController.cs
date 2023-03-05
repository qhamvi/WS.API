using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WS.API.DTO.Chapter;
using WS.API.Service;
using WS.API.Service.Implements;

namespace WS.API.Controllers
{
    [Route("chapters")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _service;
        private readonly ILogger<ChaptersController> _logger;
        public ChaptersController(IChapterService service, ILogger<ChaptersController> logger)
        {
            _service = service;
            _logger = logger;
        }
        //GET /chapters
        /// <summary>
        /// Lấy danh sách chapters
        /// </summary>
        /// <param name="request"></param>  
        [HttpGet]
        public async Task<ListChapterResponse> GetListChapterAsync([FromQuery]ListChapterRequest request)
        {
            _logger.LogInformation("Get chapters");
            var list = await _service.GetChaptersAsync(request);
            return list;
        }
        //POST /chapter
        /// <summary>
        /// Tạo 1 chapter, cập nhật chapter đó vào Story
        /// </summary>
        /// <param name="request"></param>    
        [HttpPost]
        public async Task<ChapterResponse> CreateAsync(CreateChapterRequest request)
        {
            var chapter = await _service.CreateChapterAsync(request);
            return chapter;
        }

        /// <summary>
        /// Xoa 1 chapter , Xóa chapter trong Story
        /// </summary>
        /// <param name="idChapter"></param>   
        [HttpDelete("idChapter")]
        public async Task<ActionResult> DeleteAsync(Guid idChapter)
        {
            await _service.DeleteChapterAsync(idChapter);
            return Content("Delete Sucess");
        }

        /// <summary>
        /// Cập nhật 1 chapter
        /// </summary>
        /// <param name="request"></param> 
        [HttpPut]
        public async Task<ChapterResponse> UpdateAsync(ChapterRequest request)
        {
            var chapter = await _service.UpdateChapterAsync(request);
            return chapter;
        }
    }
}
