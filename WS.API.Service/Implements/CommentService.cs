using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.Models;

namespace WS.API.Service.Implements
{
    public class CommentService : ICommentService
    {
        private const string databaseName = "webstory";
        private const string collectionName = "comments";
        private readonly IMongoCollection<Comment> _commentsCollection;
        private readonly FilterDefinitionBuilder<Comment> _filterBuilder = new ();
        
        public CommentService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _commentsCollection = database.GetCollection<Comment>(collectionName);
        }

        public async Task CreateCommentAsync(Comment comment)
        {
            await _commentsCollection.InsertOneAsync(comment);
        }

        public async Task DeleteCommentAsync(Guid idComment)
        {
            var filter = _filterBuilder.Eq(comment => comment.Id, idComment);
            await _commentsCollection.DeleteOneAsync(filter);
        }

        public async Task DeleteCommentOfMember(string idUser)
        {
            var filter = _filterBuilder.Eq(comment => comment.IdUser, idUser);
            await _commentsCollection.DeleteManyAsync(filter);

        }

        public async Task<Comment> GetCommentAsync(Guid idComment)
        {
            var filter = _filterBuilder.Eq(comment => comment.Id, idComment);
            return await _commentsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            return await _commentsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            var filter = _filterBuilder.Eq(v => v.Id, comment.Id);
            await _commentsCollection.ReplaceOneAsync(filter, comment);
        }
        
    }
}
