using System.ComponentModel.DataAnnotations;

namespace WS.API.DTOs.Topic
{
    public record TopicRequest
    {
        [MinLength(1)]
        [StringLength(50)]
        public string NameTopic { get; init; }
    }
}
