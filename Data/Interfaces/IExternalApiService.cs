using CryptoCurrencyDemoProject.Data.Models;

namespace CryptoCurrencyDemoProject.Data.Interfaces
{
    public interface IExternalApiService
    {
        Task<List<CurrencyModel>> GetCryptocurrenciesAsync();
    }
}
