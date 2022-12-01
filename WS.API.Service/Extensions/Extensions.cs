using WS.API.DTO.Chapter;
using WS.API.DTO.Comment;
using WS.API.DTO.Role;
using WS.API.DTO.Story;
using WS.API.DTO.Topic;
using WS.API.DTO.User;
using WS.API.Models;

namespace WS.API.Extensions
{
    public static class Extensions
    {
        public static CommentResponse AsCommentDto(this Comment comment)
        {
            return new CommentResponse
            {
                id = comment.Id,
                idUser = comment.IdUser,
                idStory = comment.IdStory,
                content = comment.Content,
                dateCom = comment.DateCom,
                updateCom = comment.UpdateCom
            };
        }
        public static TopicResponse AsTopicDto(this Topic topic)
        {
            return new TopicResponse
            {
                id = topic.Id,
                nameTopic = topic.NameTopic
            };
        }

        public static RoleResponse AsRoleDto(this Role role)
        {
            return new RoleResponse
            {
                Id = role.Id,
                NameRole = role.NameRole
            };
        }
        public static UserResponse AsUserDto (this User user)
        {
            return new UserResponse
            {
                id =  user.Id,
                idRole = user.IdRole,
                country = user.Country,
                createDate = user.CreateDate,
                email = user.Email,
                fullName = user.FullName,
                like = user.Like,
                history = user.History,
                password = user.Password,
                phone = user.Phone,
                PhotoFileName = user.PhotoFileName,
                username = user.Username
            };
        }
        public static StoryResponse AsStoryDto(this Story story)
        {
            return new StoryResponse()
            {
                Id = story.Id,
                Author = story.Author,
                Collector = story.Collector,
                Complete = story.Complete,
                CreateDate = story.CreateDate,
                IdCom = story.IdCom,
                ImageFileName = story.ImageFileName,
                ListChap = story.ListChap,
                ListTopic = story.ListTopic,
                NumberChap = story.NumberChap,
                PublishDate = story.PublishDate,
                Status = story.Status,
                Summary = story.Summary,
                TitleStory = story.TitleStory
            };
        }
        public static ChapterResponse AsChapterDto(this Chapter chapter)
        {
            return new ChapterResponse()
            {
                Id = chapter.Id,
                Collector = chapter.Collector,
                Content = chapter.Content,
                CreateDate = chapter.CreateDate,
                IdStory = chapter.IdStory,
                TitleChap = chapter.TitleChap
            };

        }
    }
}
