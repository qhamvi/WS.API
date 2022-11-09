using System;

namespace WS.API.Models
{
    public record Comment
    {
        public Guid Id { get; init; }

        public string IdUser { get; init; }

        public string IdStory { get; init; }

        public string Content { get; init; }

        public DateTime DateCom { get; init; }

        public DateTime UpdateCom { get; init; }
    }
}
