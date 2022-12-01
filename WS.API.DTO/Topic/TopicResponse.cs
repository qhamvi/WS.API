using System;

namespace WS.API.DTO.Topic
{
    public record TopicResponse
    {
        public Guid id { get; init; }

        public string nameTopic { get; set; }
    }
}
