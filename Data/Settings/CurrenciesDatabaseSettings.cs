using CryptoCurrencyDemoProjectTest.Data.Interfaces;

namespace CryptoCurrencyDemoProjectTest.Data.Settings
{
    public class CurrenciesDatabaseSettings : ICurrenciesDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CurrenciesCollection { get; set; }
    }
}
