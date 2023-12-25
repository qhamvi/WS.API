using Bogus;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.API.DTO;
using WS.API.DTO.Topic;
using WS.API.Extensions;
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
            //var faker = new Faker("en");
            //var topics = Enumerable.Range(1, 100000).Select(v => faker.Company.CompanyName()).ToList();
            //List<Topic> topics1 = topics.Select(v => new Topic()
            //{
            //    Id = Guid.NewGuid(),
            //    NameTopic = v
            //}).ToList();
            //await _topicsCollection.InsertManyAsync(topics1);

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
            var x = await _topicsCollection.Find(new BsonDocument()).Limit(10)
            .ToListAsync();
            return x;
        }

        public async Task<GetListTopicResponse> GetTopicsAsync(GetListTopicRequest request)
        {
            request.Page = request.Page is 0 ? 1 : request.Page;
            request.PageSize = request.PageSize is 0 ? 10 : request.PageSize;
            long count = await _topicsCollection.CountAsync(Builders<Topic>.Filter.Empty, null);

            var topics = await _topicsCollection.Find(new BsonDocument())
                .Limit(request.PageSize)
                .Skip((request.Page -1)* request.PageSize)
                .ToListAsync();
            GetListTopicResponse response = new()
            {
                Count = count,
                Results = topics.Select(v => v.AsTopicDto()).ToList(),
                Page = request.Page,
                PageSize = request.PageSize
            };
            return response;
        }

        public async Task UpdateTopicAsync(Topic topic)
        {
            var filter = _filterBuilder.Eq(existingTopic => existingTopic.Id, topic.Id);
            await _topicsCollection.ReplaceOneAsync(filter, topic);
        }
    }
}
