using CryptoCurrencyDemoProject.Data.Models;

namespace CryptoCurrencyDemoProject.Data.Interfaces
{
    public interface ICurrenciesService
    {
        Task<List<CryptocurrencyModel>> SelectAll();
        Task<PaginationResponse<CryptocurrencyModel>> SelectPage(int page = 1, int pageSize = 9);
        CryptocurrencyModel Get(string id);
        Task<List<CryptocurrencyModel>> GetTrendingCryptocurrenciesAsync();
        Task<List<CryptocurrencyModel>> GetVolumeLeadersCryptocurrenciesAsync();
        bool Insert(CryptocurrencyModel cryptocurrencyModel);
        Task<bool> InsertManyAsync(List<CryptocurrencyModel> cryptocurrencyModels);
        void Update(string id,CryptocurrencyModel cryptocurrencyModel);
        void Remove(string id);
    }
}
