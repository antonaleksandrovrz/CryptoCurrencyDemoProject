using CryptoCurrencyDemoProject.Data.Models;
using Newtonsoft.Json.Linq;

namespace CryptoCurrencyDemoProject.Data.Services
{
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;

        public ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CurrencyModel>> GetCryptocurrenciesAsync()
        {
            try
            {
                string apiUrl = "https://api.coincap.io/v2/assets";
                string response = await _httpClient.GetStringAsync(apiUrl);
                JObject jsonResponse = JObject.Parse(response);

                if (jsonResponse["data"] is JArray data)
                {
                    List<CurrencyModel>? cryptocurrencies = data.ToObject<List<CurrencyModel>>();

                    if (cryptocurrencies != null && cryptocurrencies.Count > 0)
                    {
                        return cryptocurrencies;
                    }
                }

                Console.WriteLine("Error: Unexpected or null data.");
                return new List<CurrencyModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
