using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTOs.Comment;
using WS.API.Models;

namespace WS.API.Service
{
    public interface ICommentService
    {
        Task<Comment> GetCommentAsync(Guid idComment);
        Task<IEnumerable<Comment>> GetCommentsAsync();

        Task CreateCommentAsync(Comment comment);
        Task UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(Guid idComment);
        Task DeleteCommentOfMember(string idUser);
    }
}
