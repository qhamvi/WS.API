using System;
using System.ComponentModel.DataAnnotations;

namespace WS.API.DTOs.User
{
    public class UpdateUserRequest
    {
        [MinLength(5)]
        [MaxLength(50)]
        public string Username { get; init; }
        [MinLength(5)]
        [MaxLength(50)]
        public string Password { get; init; }

        public string PhotoFileName { get; init; }

        public string FullName { get; init; }

        public string Phone { get; init; }

        public string Email { get; init; }

        public string Country { get; init; }



    }
}
