using System;

namespace WS.API.Models
{
    public record Chapter
    {
        public Guid Id { get; init; }

        public string TitleChap { get; init; }

        public string IdStory { get; init; }

        public string Collector { get; init; }

        public DateTime CreateDate { get; init; }

        public DateTime PublishDate { get; init; }

        public string Content { get; init; }

    }
}
