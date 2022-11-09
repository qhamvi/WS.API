using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.Models;

namespace WS.API.Service
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);
        Task DeleteUserAsync(Guid idUser);
        Task<User> GetUserAsync(Guid idUser);
        Task<IEnumerable<User>> GetUsersAsync();
        Task UpdateUserAsync(User user);

    }
}
