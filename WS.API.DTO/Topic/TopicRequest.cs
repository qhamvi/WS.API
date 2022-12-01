using System.ComponentModel.DataAnnotations;

namespace WS.API.DTO.Topic
{
    public record TopicRequest
    {
        [MinLength(1)]
        [StringLength(50)]
        public string NameTopic { get; init; }
    }
}
