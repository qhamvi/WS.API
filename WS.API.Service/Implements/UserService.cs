using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.Models;

namespace WS.API.Service.Implements
{
    public class UserService : IUserService
    {
        private const string _databaseName = "webstory";
        private const string _collectionName = "users";
        private readonly IMongoCollection<User> _usersCollection;
        private readonly FilterDefinitionBuilder<User> _filterBuilder = Builders<User>.Filter;

        public UserService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _usersCollection = database.GetCollection<User>(_collectionName);
        }

        public async Task CreateUserAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(Guid idUser)
        {
            var filter = _filterBuilder.Eq(user => user.Id, idUser);
            await _usersCollection.DeleteOneAsync(filter);
        }

        public async Task<User> GetUserAsync(Guid idUser)
        {
            var filter = _filterBuilder.Eq(user => user.Id, idUser);
            return await _usersCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _usersCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var filter = _filterBuilder.Eq(existingUser => existingUser.Id, user.Id);
            await _usersCollection.ReplaceOneAsync(filter, user);
        }
            
    }
}
