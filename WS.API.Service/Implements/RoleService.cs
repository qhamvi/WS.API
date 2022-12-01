using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTO.Role;
using WS.API.Models;

namespace WS.API.Service.Implements
{
    public class RoleService : IRolesService
    {
        private const string _databaseName = "webstory";
        private const string _collectionName = "roles";
        private readonly IMongoCollection<Role> _rolesCollection;


        private readonly FilterDefinitionBuilder<Role> _filterBuilder = Builders<Role>.Filter;

        public RoleService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _rolesCollection = database.GetCollection<Role>(_collectionName);
        }

        public async Task CreateRoleAsync(RoleRequest request)
        {
            Role role = new()
            {
                Id = Guid.NewGuid(),
                NameRole = request.NameRole
            };
            await _rolesCollection.InsertOneAsync(role);

        }

        public async Task DeleteRoleAsync(Guid idRole)
        {
            var filter = _filterBuilder.Eq(role => role.Id, idRole);
            await _rolesCollection.DeleteOneAsync(filter);
        }

        public async Task<Role> GetRoleAsync(Guid idRole)
        {
            var filter = _filterBuilder.Eq(role => role.Id, idRole);
            return await _rolesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Role> GetRoleExistAsync(RoleRequest request)
        {
            var filter = _filterBuilder.Eq(role => role.NameRole, request.NameRole);
            return await _rolesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _rolesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            var filter = _filterBuilder.Eq(existingTopic => existingTopic.Id, role.Id);
            await _rolesCollection.ReplaceOneAsync(filter, role);
        }
    }
}
