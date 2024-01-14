using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Models;
using MongoDB.Driver;

namespace CryptoCurrencyDemoProject.Data.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly IMongoCollection<CryptocurrencyModel> cryptoCurrencies;

        public CurrenciesService(ICurrenciesDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            cryptoCurrencies = database.GetCollection<CryptocurrencyModel>(databaseSettings.CurrenciesCollection);
        }

        public bool Insert(CryptocurrencyModel cryptocurrencyData)
        {
            try
            {
                cryptoCurrencies.InsertOne(cryptocurrencyData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> InsertManyAsync(List<CryptocurrencyModel> cryptocurrencyModels)
        {
            try
            {
                var filter = Builders<CryptocurrencyModel>.Filter.Empty; // Empty filter matches all documents
                await cryptoCurrencies.DeleteManyAsync(filter);

                await cryptoCurrencies.InsertManyAsync(cryptocurrencyModels);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<CryptocurrencyModel>> SelectAll()
        {
            var result = await cryptoCurrencies.Find(cryptoCurrency => true).ToListAsync();
            return result;
        }

        public async Task<PaginationResponse<CryptocurrencyModel>> SelectPage(int page = 1, int pageSize = 9)
        {
            var skipCount = (page - 1) * pageSize;
            var totalItems = await cryptoCurrencies.CountDocumentsAsync(Builders<CryptocurrencyModel>.Filter.Empty);

            var result = await cryptoCurrencies
                .Find(cryptoCurrency => true)
                .Skip(skipCount)
                .Limit(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new PaginationResponse<CryptocurrencyModel>
            {
                Data = result,
                TotalPages = totalPages
            };
        }


        public CryptocurrencyModel Get(string id)
        {
            return cryptoCurrencies.Find(cryptoCurrency => cryptoCurrency.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            cryptoCurrencies.DeleteOne(cryptoCurrency => cryptoCurrency.Id == id);
        }

        public void Update(string id, CryptocurrencyModel cryptocurrencyModel)
        {
            cryptoCurrencies.ReplaceOne(cryptoCurrency => cryptoCurrency.Id == id, cryptocurrencyModel);
        }

        public async Task<List<CryptocurrencyModel>> GetTrendingCryptocurrenciesAsync()
        {
            var result = await cryptoCurrencies
                .Find(cryptoCurrency => cryptoCurrency.ChangePercent24Hr > 0)
                .SortByDescending(cryptoCurrency => cryptoCurrency.ChangePercent24Hr)
                .Limit(9)
                .ToListAsync();

            return result;
        }

        public async Task<List<CryptocurrencyModel>> GetVolumeLeadersCryptocurrenciesAsync()
        {
            var result = await cryptoCurrencies
                .Find(cryptoCurrency => cryptoCurrency.VolumeUsd24Hr > 0)
                .SortByDescending(cryptoCurrency => cryptoCurrency.VolumeUsd24Hr)
                .Limit(9)
                .ToListAsync();

            return result;
        }

    }
}
