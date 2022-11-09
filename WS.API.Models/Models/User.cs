using System;
using System.Collections.Generic;

namespace WS.API.Models
{
    public record User
    {
        public Guid Id { get; init; }

        public string Username { get; init; }

        public string Password { get; init; }

        public string PhotoFileName { get; init; }
        public string IdRole { get; init; }

        public string FullName { get; init; }

        public string Phone { get; init; }

        public string Email { get; init; }

        public string Country { get; init; }

        public DateTime CreateDate { get; init; }

        public List<string> Like { get; init; }

        public List<string> History { get; init; }
    }
}
