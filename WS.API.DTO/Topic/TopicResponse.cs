using System;

namespace WS.API.DTOs.Topic
{
    public record TopicResponse
    {
        public Guid id { get; init; }

        public string nameTopic { get; set; }
    }
}
