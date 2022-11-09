using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using WS.API.DTOs.Story;
using WS.API.Models;
using static WS.API.Extensions.StoryExtension;
using WS.API.Extensions;


namespace WS.API.Service.Implements
{
    public class StoryService : IStoryService
    {
        private const string databaseName = "webstory2";
        private const string collectionName = "stories";
        private readonly IMongoCollection<Story> _storiesCollection;
        private readonly FilterDefinitionBuilder<Story> _filterBuilder = new ();

        public StoryService(IMongoClient mongoClient)
        {
             IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _storiesCollection = database.GetCollection<Story>(collectionName);
        }

        //Collector : tao truyen suu tam => status = false
        public async Task CreateStoryAsync(StoryRequest request)
        {
            Story story = new()
            {
                Id = Guid.NewGuid(),
                Author = request.Author,
                Collector = request.Collector,
                Complete = request.Complete,
                CreateDate = DateTime.Now,
                IdCom = null,
                ImageFileName = request.ImageFileName,
                ListChap = request.ListChap,
                ListTopic = request.ListTopic,
                NumberChap = request.ListChap != null ? request.ListChap.Count() : 0,
                PublishDate = null,
                Status = false,
                Summary = request.Summary,
                TitleStory = request.TitleStory
            };
            await _storiesCollection.InsertOneAsync(story);
        }

        public async Task DeleteStoryAsync(Guid idStory)
        {
            var filter = _filterBuilder.Eq(v => v.Id, idStory);
            await _storiesCollection.DeleteOneAsync(filter);
        }

        public async Task<GetListStoryResponse> GetStoriesAsync(GetListStoryRequest request)
        {
            var listStory = await _storiesCollection.Find(new BsonDocument()).ToListAsync();
            Expression<Func<Story, bool>> searchFilterExpression = string.IsNullOrEmpty(request.Search)
            ? v => true
            : v => v.TitleStory.Contains(request.Search) || v.Author.Contains(request.Search);

            Expression<Func<Story, bool>> statusFilterExpression = request.StatusStory.HasValue
            ? v => v.Status == request.StatusStory
            : v => true;

            Expression<Func<Story, bool>> completeFilterExpression = request.IsComplete.HasValue
            ? v => v.Complete == request.IsComplete
            : v => true;

            var listStory2 = listStory.AsQueryable()
                            .Where(searchFilterExpression)
                            .Where(statusFilterExpression)
                            .Where(completeFilterExpression)
                            .Select(v => new StoryResponse()
                            {
                                Id = v.Id,
                                Author = v.Author,
                                Collector = v.Collector,
                                Complete = v.Complete,
                                CreateDate = v.CreateDate,
                                IdCom = v.IdCom,
                                ImageFileName = v.ImageFileName,
                                ListChap = v.ListChap,
                                ListTopic = v.ListTopic,
                                NumberChap = v.NumberChap,
                                PublishDate = v.PublishDate,
                                Status = v.Status,
                                Summary = v.Summary,
                                TitleStory = v.TitleStory
                            })
                            .ToList();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var sortName = string.IsNullOrEmpty(request.Sort) ? "" : request.Sort;

            var listPaginationRequest = (request.IsAscSorting == true) ? listStory2.OrderBy(KeySortParameters[sortName])
               .Skip(numberPageSize * (numberPage - 1)).Take(numberPageSize).ToList() :
           (request.IsAscSorting == false) ? listStory2.OrderByDescending(KeySortParameters[sortName] )
               .Skip(numberPageSize * (numberPage - 1)).Take(numberPageSize).ToList() :
           listStory2.OrderBy(v => v.PublishDate).Skip(numberPageSize * (numberPage - 1)).Take(numberPageSize).ToList();

            return new ()
            {
                Count = listStory2.Count(),
                Page = numberPage,
                PageSize = numberPageSize,
                Results = listPaginationRequest
            };
        }


        public async Task<Story> GetStoryAsync(Guid idStory)
        {
            var filter = _filterBuilder.Eq(story => story.Id, idStory);
            return await _storiesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateStoryAsync(Story story)
        {
            var filter = _filterBuilder.Eq(v => v.Id, story.Id);
            await _storiesCollection.ReplaceOneAsync(filter, story);
        }

        public async Task<StoryResponse> UpdateStatusStoryAsync(Guid idStory)
        {
            var filter = _filterBuilder.Eq(story => story.Id, idStory);
            var existing = await _storiesCollection.Find(filter).SingleOrDefaultAsync();
            var copy = existing with 
            {
                Status = true
            };
            await _storiesCollection.ReplaceOneAsync(filter, copy);
            return copy.AsStoryDto();

        }

        public async Task<StoryResponse> UpdateChapterStoryAsync(UpdateChapterStoryRequest request)
        {
            var filter = _filterBuilder.Where(s => s.Id == request.IdStory);
            var existStory = await _storiesCollection.Find(filter).SingleOrDefaultAsync();
            existStory.ListChap.AddRange(request.ListChap);
            var copy = existStory with
            {
                ListChap = existStory.ListChap,
                NumberChap = existStory.ListChap.Count(),
            };
            await _storiesCollection.ReplaceOneAsync(filter, copy);
            return copy.AsStoryDto();
                
        }

        public async Task<StoryResponse> UpdateStoryAsync(UpdateStoryRequest story)
        {
            var filter = _filterBuilder.Where(s => s.Id == story.IdStory);
            var existStory = await _storiesCollection.Find(filter).SingleOrDefaultAsync();
            var copy = existStory with
            {
                TitleStory = story.TitleStory,
                Author = story.Author,
                Collector = story.Collector,
                Complete = story.Complete,
                ImageFileName = story.ImageFileName,
                Summary = story.Summary
            };
            await _storiesCollection.ReplaceOneAsync(filter, copy);
            return copy.AsStoryDto();
        }

        public async Task<StoryResponse> DeleteChapterStoryAsync(UpdateChapterStoryRequest request)
        {
            var filter = _filterBuilder.Where(s => s.Id == request.IdStory);
            var existStory = await _storiesCollection.Find(filter).SingleOrDefaultAsync();
            existStory.ListChap.RemoveAll(v => request.ListChap.Any(s => s == v));
            var copy = existStory with
            {
                ListChap = existStory.ListChap,
                NumberChap = existStory.ListChap.Count(),
            };
            await _storiesCollection.ReplaceOneAsync(filter, copy);
            return copy.AsStoryDto();
        }
    }
}