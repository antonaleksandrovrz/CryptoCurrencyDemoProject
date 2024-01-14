using CryptoCurrencyDemoProject.Data.Interfaces;

namespace CryptoCurrencyDemoProject.Data
{
    public class CurrenciesDatabaseSettings : ICurrenciesDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CurrenciesCollection { get; set; }
    }
}
