using System;

namespace WS.API.Models
{
    public record Topic
    {
        public Guid Id { get; set; }

        public string NameTopic { get; set; }
    }
}
