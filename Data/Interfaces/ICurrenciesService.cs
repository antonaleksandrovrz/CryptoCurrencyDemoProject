using CryptoCurrencyDemoProject.Data.Models;

namespace CryptoCurrencyDemoProject.Data.Interfaces
{
    public interface ICurrenciesService
    {
        Task<List<CurrencyModel>> SelectAll();
        Task<PaginationResponse<CurrencyModel>> SelectPage(int page = 1, int pageSize = 9);
        CurrencyModel Get(string id);
        Task<List<CurrencyModel>> GetTrendingCryptocurrenciesAsync();
        Task<List<CurrencyModel>> GetVolumeLeadersCryptocurrenciesAsync();
        void Insert(CurrencyModel cryptocurrencyModel);
        Task<bool> InsertManyAsync(List<CurrencyModel> cryptocurrencyModels);
        void Update(string id, CurrencyModel cryptocurrencyModel);
        void Remove(string id);
    }
}
