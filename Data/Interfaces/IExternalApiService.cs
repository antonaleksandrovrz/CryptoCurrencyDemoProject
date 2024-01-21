using CryptoCurrencyDemoProjectTest.Data.Models;

namespace CryptoCurrencyDemoProjectTest.Data.Interfaces
{
    public interface IExternalApiService
    {
        Task<List<CurrencyModel>> GetCryptocurrenciesAsync();
    }
}
