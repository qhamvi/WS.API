using System.ComponentModel.DataAnnotations;

namespace WS.API.DTO.User
{
    public class UserRequest
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string Username { get; init; }
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; init; }
    }
}
