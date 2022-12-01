namespace WS.API.DTO.Comment
{
    public class CommentRequest
    {
        public string IdUser { get; init; }
        public string IdStory { get; init; }
        public string Content { get; init; }
    }
}
