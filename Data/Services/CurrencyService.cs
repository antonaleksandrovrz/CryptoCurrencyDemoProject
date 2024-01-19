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

        public void Insert(CurrencyModel cryptocurrencyData)
        {
            try
            {
                cryptoCurrencies.InsertOne(cryptocurrencyData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert single cryptocurrencyData. Ex message:{ex.Message}");
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
            catch (Exception ex)
            {
                throw new Exception($"Failed to insert multiple cryptocurrencyData. Ex message:{ex.Message}");
            }
        }

        public async Task<List<CurrencyModel>> SelectAll()
        {
            try
            {
                List<CurrencyModel> result = await cryptoCurrencies.Find(cryptoCurrency => true).ToListAsync();
                if(result.Count == 0)
                {
                    throw new Exception("No cryptocurrency Found");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select cryptocurrencyData. Ex message:{ex.Message}");
            }
            
        }

        public async Task<PaginationResponse<CurrencyModel>> SelectPage(int page = 1, int pageSize = 9)
        {
            try
            {
                int skipCount = (page - 1) * pageSize;
                long totalItems = await cryptoCurrencies.CountDocumentsAsync(Builders<CurrencyModel>.Filter.Empty);

                List<CurrencyModel> result = await cryptoCurrencies
                    .Find(cryptoCurrency => true)
                    .Skip(skipCount)
                    .Limit(pageSize)
                    .ToListAsync();

                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                if (result.Count == 0)
                {
                    throw new Exception("No cryptocurrency Found");
                }

                return new PaginationResponse<CurrencyModel>
                {
                    Data = result,
                    TotalPages = totalPages
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select cryptocurrencyData page. Ex message:{ex.Message}");
            }
        }


        public CurrencyModel Get(string id)
        {
            try
            {
                CurrencyModel currency = cryptoCurrencies.Find(cryptoCurrency => cryptoCurrency.Id == id).FirstOrDefault();

                if (currency == null)
                {
                    throw new Exception($"No currency found with id: {id}");
                }

                return currency;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get cryptocurrencyData. Ex message:{ex.Message}");
            }
            
        }

        public void Remove(string id)
        {
            try
            {
                cryptoCurrencies.DeleteOne(cryptoCurrency => cryptoCurrency.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to remove cryptocurrencyData. Ex message:{ex.Message}");
            }
            
        }

        public void Update(string id, CurrencyModel cryptocurrencyModel)
        {
            try
            {
                cryptoCurrencies.ReplaceOne(cryptoCurrency => cryptoCurrency.Id == id, cryptocurrencyModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update cryptocurrencyData. Ex message:{ex.Message}");
            }
            
        }

        public async Task<List<CurrencyModel>> GetTrendingCryptocurrenciesAsync()
        {
            try
            {
                List<CurrencyModel> result = await cryptoCurrencies
                .Find(cryptoCurrency => cryptoCurrency.ChangePercent24Hr > 0)
                .SortByDescending(cryptoCurrency => cryptoCurrency.ChangePercent24Hr)
                .Limit(9)
                .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    throw new Exception($"No cryptocurrencyData found.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get trending cryptocurrencyData. Ex message:{ex.Message}");
            }
        }

        public async Task<List<CurrencyModel>> GetVolumeLeadersCryptocurrenciesAsync()
        {
            try
            {
                List<CurrencyModel> result = await cryptoCurrencies
                .Find(cryptoCurrency => cryptoCurrency.VolumeUsd24Hr > 0)
                .SortByDescending(cryptoCurrency => cryptoCurrency.VolumeUsd24Hr)
                .Limit(9)
                .ToListAsync();

                if (result == null || result.Count == 0)
                {
                    throw new Exception($"No cryptocurrencyData found.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get leading cryptocurrencyData. Ex message:{ex.Message}");
            }
        }

    }
}
