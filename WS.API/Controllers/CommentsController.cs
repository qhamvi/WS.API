using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.API.DTO.Comment;
using WS.API.DTO.Response;
using WS.API.Extensions;
using WS.API.Models;
using WS.API.Service.Implements;

namespace WS.API.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _service;
        public CommentsController(CommentService service)
        {
            _service = service;
        }

        
        /// <summary>
        /// Lấy giá trị tất cả comment
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<CommentResponse>> GetCommentsAsync()
        {
            
            return (await _service.GetCommentsAsync())
                .Select(v => v.AsCommentDto());
        }

        /// <summary>
        /// Lấy giá trị 1 comment theo id
        /// </summary>
        /// <param name="idComment"></param>    
        [HttpGet("idComment")]
        public async Task<ActionResult<CommentResponse>> GetCommentAsync(Guid idComment)
        {
            var comment = await _service.GetCommentAsync(idComment);
            if(comment is null)
            {
                return NotFound();
            }
            return comment.AsCommentDto();
        }

        /// <summary>
        /// User tạo comment 
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CommentResponse>> CreateCommentAsync(CommentRequest request)
        {
            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                IdUser = request.IdUser,
                IdStory = request.IdStory,
                Content = request.Content,
                DateCom = DateTime.UtcNow,
                UpdateCom = DateTime.UtcNow
            };
            await _service.CreateCommentAsync(comment);
            return Ok(comment.AsCommentDto());
        }

        /// <summary>
        /// User cập nhật lại comment
        /// </summary>
        /// <param name="idComment"></param>    
        [HttpPut("idComment")]
        public async Task<ActionResult<CommentResponse>> UpdateCommentAsync(Guid idComment, CommentRequest request)
        {
            var existComment = await _service.GetCommentAsync(idComment);
            if(existComment is null)
            {
                return NotFound(
                    new ReponseMessage()
                {
                    message = "Not Found"
                }); 
            }
            Comment updateComment = existComment with
            {
                IdStory = request.IdStory,
                IdUser = request.IdUser,
                Content = request.Content,
                UpdateCom = DateTime.UtcNow
            };
            await _service.UpdateCommentAsync(updateComment);
            return Content("Update Success");
        }

        /// <summary>
        /// User xóa comment
        /// </summary>
        /// <param name="idComment"></param>    
        [HttpDelete("idComment")]
        public async Task<ActionResult> DeleteCommentAsync(Guid idComment)
        {
            var existComment = await _service.GetCommentAsync(idComment);
            if(existComment is null)
            {
                return NotFound(new ReponseMessage()
                {
                    message = "Not Found"
                });
            }
            await _service.DeleteCommentAsync(idComment);
            return Content("Delete Success");
        }
        
    }
}
