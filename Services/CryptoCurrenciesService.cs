using CryptoCurrencyDemoProject.Data.Models;
using Newtonsoft.Json.Linq;
using System;

namespace CryptoCurrencyDemoProject.Services
{
    public class CryptoCurrenciesService
    {
        private readonly HttpClient _httpClient;

        public CryptoCurrenciesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CryptocurrencyModel>> GetCryptocurrenciesAsync()
        {
            try
            {
                var apiUrl = "https://api.coincap.io/v2/assets";
                var response = await _httpClient.GetStringAsync(apiUrl);
                var jsonResponse = JObject.Parse(response);

                if (jsonResponse["data"] is JArray data)
                {
                    var cryptocurrencies = data.ToObject<List<CryptocurrencyModel>>();

                    if (cryptocurrencies != null && cryptocurrencies.Count > 0)
                    {
                        return cryptocurrencies;
                    }
                }

                Console.WriteLine("Error: Unexpected or null data.");
                return new List<CryptocurrencyModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
