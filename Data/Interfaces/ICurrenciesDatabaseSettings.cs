namespace CryptoCurrencyDemoProject.Data.Interfaces
{
    public interface ICurrenciesDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CurrenciesCollection { get; set; }
    }
}
