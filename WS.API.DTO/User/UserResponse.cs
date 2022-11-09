using System;
using System.Collections.Generic;

namespace WS.API.DTOs.User
{
    public record UserResponse
    {
        public Guid id { get; init; }

        public string username { get; init; }

        public string password { get; init; }

        public string PhotoFileName { get; init; }
        public string idRole { get; init; }

        public string fullName { get; init; }

        public string phone { get; init; }

        public string email { get; init; }

        public string country { get; init; }

        public DateTime createDate { get; init; }

        public List<string> like { get; init; }

        public List<string> history { get; init; }
    }
}
