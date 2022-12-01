using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WS.API.DTO.User;
using WS.API.Extensions;
using WS.API.Models;
using WS.API.Service.Implements;

namespace WS.API.Controllers
{
    [Route("users")]
    [ApiController]


    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _service;
        private readonly CommentService _commentService;
        private readonly IWebHostEnvironment _env;

        public UsersController(IConfiguration configuration, UserService service, 
        CommentService commentService, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _service = service;
            _commentService = commentService;
            _env = env;
        }

        /// <summary>
        /// Lấy danh sách user trong hệ thống
        /// </summary>
        /// <param name="idComment"></param>    
        [HttpGet]
        public async Task<IEnumerable<UserResponse>> GetUsersAsync()
        {
            var users = (await _service.GetUsersAsync()).Select(user => user.AsUserDto());
            return users;
        }

        /// <summary>
        /// Lấy user theo idUser
        /// </summary>
        /// <param name="idUser"></param>    
        [HttpGet("{idUser}")]
        public async Task<ActionResult<UserResponse>> GetUserAsync(Guid idUser)
        {
            var user = await _service.GetUserAsync(idUser);

            if (user is null)
            {
                return NotFound();
            }
            return user.AsUserDto() ;
        }

        /// <summary>
        /// Tạo tài khoản cho Admin
        /// </summary>
        /// <param name="request"></param>    
        [HttpPost("admins")]
        public async Task<ActionResult<UserResponse>> CreateAdminAsync(UserRequest request)
        {
            User user = new User {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                IdRole = "Admin",
                PhotoFileName = "null.png",
                FullName = "",
                Phone = "",
                Email = "",
                Country = "",
                CreateDate = DateTime.Now,
                Like = null,
                History = null
            };
            await _service.CreateUserAsync(user);
            return Ok(user.AsUserDto());
        }

        /// <summary>
        /// Tạo tài khoản cho Member
        /// </summary>
        /// <param name="request"></param>   
        [HttpPost("members")]
        public async Task<ActionResult<UserResponse>> CreateMemberAsync(UserRequest request)
        {
            User user = new User {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                IdRole = "Member",
                PhotoFileName = "null.png",
                FullName = "",
                Phone = "",
                Email = "",
                Country = "",
                CreateDate = DateTime.Now,
                Like = null,
                History = null
            };
            await _service.CreateUserAsync(user);
            return Ok(user.AsUserDto());
        }

        /// <summary>
        /// Cập nhật thông tin cho user
        /// </summary>
        [HttpPut("{idUser}")]
        public async Task<ActionResult<UserResponse>> UpdateUserAsync(Guid idUser,UpdateUserRequest request)
        {
            var existingUser = await _service.GetUserAsync(idUser);
            if(existingUser is null)
            {
                return NotFound();
            }
            User updateUser = existingUser with
            {
                Username = request.Username,
                Password = request.Password,
                PhotoFileName = request.PhotoFileName,
                FullName = request.FullName,
                Phone = request.Phone,
                Email = request.Email,
                Country = request.Country,
                
            };
            await _service.UpdateUserAsync(updateUser);
            return Ok(updateUser.AsUserDto());
        }

        /// <summary>
        /// Thêm ảnh cho User
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="request"></param>
        [HttpPut("{idUser}/image")]
        public async Task<ActionResult<UserResponse>> UpdateImageAsync(Guid idUser,UpdatePhotoRequest request)
        {
            var existingUser = await _service.GetUserAsync(idUser);
            if(existingUser is null)
            {
                return NotFound();
            }
            User updateUser = existingUser with
            {
                PhotoFileName = request.PhotoFileName,
            };
            await _service.UpdateUserAsync(updateUser);
            return Ok(updateUser.AsUserDto());
        }

        //PUT /users /like/{idUser} // Cap nhat gia tri truyen (yeu thich)
        [HttpPut("like/{idUser}")]
        public async Task<ActionResult<UserResponse>> UpdateLikeAsync(Guid idUser,UpdateLikeRequest request)
        {
            var existingUser = await _service.GetUserAsync(idUser);
            if(existingUser is null)
            {
                return NotFound();
            }
            var listStoryLike = existingUser.Like;
            listStoryLike.Add(request.IdStory);

            User updateUser = existingUser with
            {

                Like = listStoryLike
                
            };
            await _service.UpdateUserAsync(updateUser);
            return NoContent();
        }
        
        /// <summary>
        /// Đánh dấu chương của các truyện 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="request"></param>
        [HttpPut("history/{idUser}")]
        public async Task<ActionResult<UserResponse>> UpdateHistoryAsync(Guid idUser, UpdateHistoryRequest request)
        {
            var existingUser = await _service.GetUserAsync(idUser);
            if (existingUser is null)
            {
                return NotFound();
            }
            var lisChapterHistory = existingUser.History;
            lisChapterHistory.Add(request.IdChapter);
            User updateUser = existingUser with 
            {
                History = lisChapterHistory
            };
            await _service.UpdateUserAsync(updateUser);
            return Ok(updateUser.AsUserDto());
        }
        
        /// <summary>
        /// Xóa user dựa theo idUser
        /// </summary>
        /// <param name="idUser"></param>
        [HttpDelete("{idUser}")]
        public async Task<ActionResult<UserResponse>> DeleteUserAsync(Guid idUser)
        {
            var existingUser = await _service.GetUserAsync(idUser);
            if(existingUser is null)
            {
                return NotFound() ;
            }
            await _service.DeleteUserAsync(idUser);
            await _commentService.DeleteCommentOfMember(idUser.ToString()); 
            //Todo: Delete comment in Story
            return Content("Delete success");
        }

        /// <summary>
        /// Lưu ảnh của user
        /// </summary>
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("cp.png");
                
            }
        }
    }
}
