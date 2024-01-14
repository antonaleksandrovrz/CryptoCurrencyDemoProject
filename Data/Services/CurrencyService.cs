using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Models;
using MongoDB.Driver;

namespace CryptoCurrencyDemoProject.Data.Services
{
    public class CurrencyService : ICurrenciesService
    {
        private readonly IMongoCollection<CurrencyModel> cryptoCurrencies;

        public CurrencyService(ICurrenciesDatabaseSettings databaseSettings, IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseSettings.DatabaseName);
            cryptoCurrencies = database.GetCollection<CurrencyModel>(databaseSettings.CurrenciesCollection);
        }

        public bool Insert(CurrencyModel cryptocurrencyData)
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

        public async Task<bool> InsertManyAsync(List<CurrencyModel> cryptocurrencyModels)
        {
            try
            {
                FilterDefinition<CurrencyModel> filter = Builders<CurrencyModel>.Filter.Empty; // Empty filter matches all documents
                _ = await cryptoCurrencies.DeleteManyAsync(filter);

                await cryptoCurrencies.InsertManyAsync(cryptocurrencyModels);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<CurrencyModel>> SelectAll()
        {
            List<CurrencyModel> result = await cryptoCurrencies.Find(cryptoCurrency => true).ToListAsync();
            return result;
        }

        public async Task<PaginationResponse<CurrencyModel>> SelectPage(int page = 1, int pageSize = 9)
        {
            int skipCount = (page - 1) * pageSize;
            long totalItems = await cryptoCurrencies.CountDocumentsAsync(Builders<CurrencyModel>.Filter.Empty);

            List<CurrencyModel> result = await cryptoCurrencies
                .Find(cryptoCurrency => true)
                .Skip(skipCount)
                .Limit(pageSize)
                .ToListAsync();

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new PaginationResponse<CurrencyModel>
            {
                Data = result,
                TotalPages = totalPages
            };
        }


        public CurrencyModel Get(string id)
        {
            return cryptoCurrencies.Find(cryptoCurrency => cryptoCurrency.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _ = cryptoCurrencies.DeleteOne(cryptoCurrency => cryptoCurrency.Id == id);
        }

        public void Update(string id, CurrencyModel cryptocurrencyModel)
        {
            _ = cryptoCurrencies.ReplaceOne(cryptoCurrency => cryptoCurrency.Id == id, cryptocurrencyModel);
        }

        public async Task<List<CurrencyModel>> GetTrendingCryptocurrenciesAsync()
        {
            List<CurrencyModel> result = await cryptoCurrencies
                .Find(cryptoCurrency => cryptoCurrency.ChangePercent24Hr > 0)
                .SortByDescending(cryptoCurrency => cryptoCurrency.ChangePercent24Hr)
                .Limit(9)
                .ToListAsync();

            return result;
        }

        public async Task<List<CurrencyModel>> GetVolumeLeadersCryptocurrenciesAsync()
        {
            List<CurrencyModel> result = await cryptoCurrencies
                .Find(cryptoCurrency => cryptoCurrency.VolumeUsd24Hr > 0)
                .SortByDescending(cryptoCurrency => cryptoCurrency.VolumeUsd24Hr)
                .Limit(9)
                .ToListAsync();

            return result;
        }

    }
}
