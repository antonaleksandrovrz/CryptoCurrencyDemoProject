using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Models;
using Newtonsoft.Json.Linq;

namespace CryptoCurrencyDemoProject.Data.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IExternalApiSettings externalApiSettings;

        public ExternalApiService(IExternalApiSettings externalApiSettings,HttpClient httpClient)
        {
            _httpClient = httpClient;
            this.externalApiSettings = externalApiSettings;
        }

        public async Task<List<CurrencyModel>> GetCryptocurrenciesAsync()
        {
            try
            {
                string response = await _httpClient.GetStringAsync(externalApiSettings.ApiEndpoint);
                JObject jsonResponse = JObject.Parse(response);

                if (jsonResponse["data"] is JArray data)
                {
                    List<CurrencyModel>? cryptocurrencies = data.ToObject<List<CurrencyModel>>();

                    if (cryptocurrencies != null && cryptocurrencies.Count > 0)
                    {
                        return cryptocurrencies;
                    }

                    else
                    {
                        throw new Exception("Unexpected or null data.");
                    }
                }

                throw new Exception("Unexpected or null data.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get cryptocurrencyData. Ex message:{ex.Message}");
            }
        }
    }
}
