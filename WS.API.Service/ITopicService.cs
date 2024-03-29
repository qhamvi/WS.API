﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTO.Topic;
using WS.API.Models;

namespace WS.API.Service
{
    public interface ITopicService
    {
        Task<Topic> GetTopicAsync(Guid idTopic);
        Task<Topic> GetTopicExist(TopicRequest request);
        Task<IEnumerable<Topic>> GetTopicsAsync();
        Task<GetListTopicResponse> GetTopicsAsync(GetListTopicRequest request);


        Task CreateTopicAsync(TopicRequest topic);
        Task UpdateTopicAsync(Topic topic);
        Task DeleteTopicAsync(Guid idTopic);
    }
}
