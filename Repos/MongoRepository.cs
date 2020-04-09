using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationApi.Repos
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }
        public async Task DeleteAsync(T entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<T> GetAsync(string id)
        {
            return await _collection.Find<T>(book => book.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAsync()
        {
            return await _collection.Find(reservation => true).ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(book => book.Id == entity.Id, entity);
        }
    }
}
