using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WS.API.DTO.Chapter;
using WS.API.Extensions;
using WS.API.Models;

namespace WS.API.Service.Implements
{
    public class ChapterService : IChapterService
    {
        private const string databaseName = "webstory";
        private const string collectionName = "chapters";
        private readonly IMongoCollection<Chapter> _chaptersCollection;
        private readonly IMongoCollection<Story> _storiesCollection;
        private readonly FilterDefinitionBuilder<Chapter> _filterBuilder = Builders<Chapter>.Filter;
        private readonly FilterDefinitionBuilder<Story> _storyFilterBuilder = Builders<Story>.Filter;


        public ChapterService(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            _chaptersCollection = database.GetCollection<Chapter>(collectionName);
            _storiesCollection = database.GetCollection<Story>("stories");
        }

        public async Task<ChapterResponse> CreateChapterAsync(CreateChapterRequest request)
        {
            Chapter chapter = new Chapter()
            {
                Id = Guid.NewGuid(),
                Collector = request.Collector,
                Content = request.Content,
                CreateDate = DateTime.Now,
                IdStory = request.IdStory,
                TitleChap = request.TitleChap
            };
            //Tao 1 Chapter
            await _chaptersCollection.InsertOneAsync(chapter);
            //Loc tim story
            var filterStory = _storyFilterBuilder.Where(s => s.Id == Guid.Parse(request.IdStory));
            var existStory = await _storiesCollection.Find(filterStory).SingleOrDefaultAsync();
            List<string> listChap = new();
            if(existStory.ListChap is null)
            {
                listChap.Add(chapter.Id.ToString());
            }
            else
            {
                existStory.ListChap.Add(chapter.Id.ToString());
                listChap = existStory.ListChap;
            }
            
            var copy = existStory with
            {
                ListChap = listChap,
                NumberChap = listChap.Count(),
            };
            //Cap nhat story
            await _storiesCollection.ReplaceOneAsync(filterStory, copy);
            return chapter.AsChapterDto();
        }

        public async Task DeleteChapterAsync(Guid idChapter)
        {
            //Loc chapter
            var filterChapter = _filterBuilder.Eq(v => v.Id, idChapter);
            //Loc Story
            var filterStory = _storyFilterBuilder.Where(s => s.ListChap.Contains(idChapter.ToString()));
            var existStory = await _storiesCollection.Find(filterStory).SingleOrDefaultAsync();
            if(existStory != null)
            {
                existStory.ListChap.Remove(idChapter.ToString());
                await _storiesCollection.ReplaceOneAsync(filterStory, existStory);
            }    
            await _chaptersCollection.DeleteOneAsync(filterChapter);
        }

        public async Task<ChapterResponse> GetChapterAsync(Guid idChapter)
        {
            var filter = _filterBuilder.Eq(v => v.Id, idChapter);
            var chapter = await _chaptersCollection.Find(filter).SingleOrDefaultAsync();
            return chapter.AsChapterDto();
        }

        public async Task<ListChapterResponse> GetChaptersAsync(ListChapterRequest request)
        {
            var listChap = await _chaptersCollection.Find(new BsonDocument()).ToListAsync();
            Dictionary<string, Func<Chapter, object>> KeySortParameters = new()
            {
                 {"createDate", x => x.CreateDate}
            };
            Expression<Func<Chapter, bool>> storiesFilterExpression = string.IsNullOrEmpty(request.IdStory)
            ? v => true
            : v => v.IdStory == request.IdStory;
            var chapters = listChap
                .AsQueryable<Chapter>()
                .Where(storiesFilterExpression)
                .ToList();

            var numberPage = request.Page <= 0 ? 1 : request.Page;
            var numberPageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var sortName = string.IsNullOrEmpty(request.Sort) ? "" : request.Sort;

            var listPaginationRequest = (request.IsAscSorting == true) ? chapters.OrderBy(KeySortParameters[sortName])
               .Skip(numberPageSize * (numberPage - 1)).Take(numberPageSize).ToList() :
           (request.IsAscSorting == false) ? chapters.OrderByDescending(KeySortParameters[sortName])
               .Skip(numberPageSize * (numberPage - 1)).Take(numberPageSize).ToList() :
           chapters.OrderBy(v => v.CreateDate).Skip(numberPageSize * (numberPage - 1)).Take(numberPageSize).ToList();

            return new ListChapterResponse()
            {
                Count = chapters.Count,
                Page = numberPage,
                PageSize = numberPageSize,
                Results = listPaginationRequest.Select(v => v.AsChapterDto()).ToList()
            };

        }

        public async Task<ChapterResponse> UpdateChapterAsync(ChapterRequest request)
        {
            var filter = _filterBuilder.Eq(v => v.Id, request.Id);
            var chapterExist = await _chaptersCollection.Find(filter).SingleOrDefaultAsync();
            var copy = chapterExist with
            {
                Collector = request.Collector,
                Content = request.Content,
                TitleChap = request.TitleChap,
                IdStory = request.IdStory
            };
            await _chaptersCollection.ReplaceOneAsync(filter, copy);
            return copy.AsChapterDto();
        }
    }
}
