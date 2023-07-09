using CosmosSample.Models;

namespace CosmosSample.Interfaces
{
    public interface ICosmosSampleService
    {
        Task<IEnumerable<CosmosSampleModel>> GetAsync(string query = null);
        Task<CosmosSampleModel> GetByIdAsync(string id, string partitionKey);
        Task AddAsync(CosmosSampleModel item);
        Task UpdateAsync(string id, CosmosSampleModel item);
        Task DeleteAsync(string id, string partitionKey);
    }
}
