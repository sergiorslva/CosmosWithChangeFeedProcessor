using CosmosSample.Interfaces;
using CosmosSample.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosSample.Services
{
    public class CosmosSampleService : ICosmosSampleService
    {
        private readonly Container _container;
        public CosmosSampleService(CosmosClient cosmosClient,
        string databaseName,
        string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(CosmosSampleModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Type));
        }

        public async Task DeleteAsync(string id, string partitionKey)
        {
            await _container.DeleteItemAsync<CosmosSampleModel>(id, new PartitionKey(partitionKey));
        }
        
        public async Task<CosmosSampleModel> GetByIdAsync(string id, string partitionKey)
        {
            var response = await _container.ReadItemAsync<CosmosSampleModel>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }

        public async Task<IEnumerable<CosmosSampleModel>> GetAsync(string queryString = null)
        {
            if (queryString == null)
            {
                queryString = "select * from c";
            }

            var query = _container.GetItemQueryIterator<CosmosSampleModel>(new QueryDefinition(queryString));
            var results = new List<CosmosSampleModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, CosmosSampleModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(item.Type));
        }    
    }
}
