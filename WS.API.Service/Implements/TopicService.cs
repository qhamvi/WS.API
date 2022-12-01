using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTO.Topic;
using WS.API.Models;

namespace WS.API.Service.Implements
{
    public class TopicService : ITopicService
    {
        private const string _databaseName = "webstory";
        private const string _collectionName = "topics";
        private readonly IMongoCollection<Topic> _topicsCollection;
        

        private readonly FilterDefinitionBuilder<Topic> _filterBuilder = Builders<Topic>.Filter;

        public TopicService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _topicsCollection = database.GetCollection<Topic>(_collectionName);
        }

        public async Task CreateTopicAsync(TopicRequest request)
        {
            Topic topic = new()
            {
                Id = Guid.NewGuid(),
                NameTopic = request.NameTopic
            };
            await _topicsCollection.InsertOneAsync(topic);
        }

        public async Task DeleteTopicAsync(Guid idTopic)
        {
            var filter = _filterBuilder.Eq(topic => topic.Id, idTopic);
            await _topicsCollection.DeleteOneAsync(filter);
        }

        public async Task<Topic> GetTopicAsync(Guid idTopic)
        {
            var filter = _filterBuilder.Eq(topic => topic.Id, idTopic);
            return await _topicsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Topic> GetTopicExist(TopicRequest request)
        {
            var filter = _filterBuilder.Eq(existingTopic => existingTopic.NameTopic, request.NameTopic);
            return await _topicsCollection.Find(filter).SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<Topic>> GetTopicsAsync()
        {
            return await _topicsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public Task<GetListTopicResponse> GetTopicsAsync(GetListTopicRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateTopicAsync(Topic topic)
        {
            var filter = _filterBuilder.Eq(existingTopic => existingTopic.Id, topic.Id);
            await _topicsCollection.ReplaceOneAsync(filter, topic);
        }
    }
}
