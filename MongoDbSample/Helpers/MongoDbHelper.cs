using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbSample.Helpers
{
    public class MongoDbHelper
    {
        private IMongoDatabase db;
        public MongoDbHelper(string dbname)
        {
            var client = new MongoClient();

            db = client.GetDatabase(dbname);
        }
        public async Task<List<T>> LoadDatas<T>(string Table)
        {
            var collection = db.GetCollection<T>(Table);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task<T> GetByID<T>(string Table, Guid id)
        {
            var collection = db.GetCollection<T>(Table);
            var filter = Builders<T>.Filter.Eq("id", id);
            return (await collection.FindAsync(filter)).First();
        }
        public async Task Insert<T>(string Table,T data)
        {
            var collection = db.GetCollection<T>(Table);
            await collection.InsertOneAsync(data);
        }
        public async Task InsertOrUpdate<T>(string Table, Guid id,T data)
        {
            var collection = db.GetCollection<T>(Table);
            await collection.ReplaceOneAsync(new BsonDocument("_id", id), data, new ReplaceOptions { IsUpsert = true });
        }
        public async Task Delete<T>(string table,Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            await collection.DeleteOneAsync(filter);
        }
       
    }
}
