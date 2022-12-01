using System;

namespace WS.API.DTO.Comment
{
    public class CommentResponse 
    {
        public Guid id { get; init; }

        public string idUser { get; init; }

        public string idStory { get; init; }

        public string content { get; init; }

        public DateTime dateCom { get; init; }

        public DateTime updateCom { get; init; }
    }
}
