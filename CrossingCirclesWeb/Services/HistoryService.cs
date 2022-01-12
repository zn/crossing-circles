using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Models;
using MongoDB.Driver;

namespace CrossingCirclesWeb.Services
{
    public class HistoryService
    {
        private readonly IMongoCollection<HistoryItem> _historyCollection;
        
        public HistoryService(IOptions<CrossingCirclesDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _historyCollection = mongoDatabase.GetCollection<HistoryItem>(
                databaseSettings.Value.HistoryCollectionName);
        }

        /// <summary>
        /// Returns the last 10 items from history
        /// </summary>
        public async Task<List<HistoryItem>> GetLast(int count = 5)
        {
            return await _historyCollection.Find(_ => true)
                .SortByDescending(x => x.InsertedDate)
                .Limit(count)
                .ToListAsync();
        }

        public async Task SaveItem(HistoryItem historyItem)
        {
            await _historyCollection.InsertOneAsync(historyItem);
        }
    }
}