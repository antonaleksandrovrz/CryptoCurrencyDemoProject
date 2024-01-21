using CryptoCurrencyDemoProjectTest.Data.Interfaces;

namespace CryptoCurrencyDemoProjectTest.Data.Settings
{
    public class ExternalApiSettings : IExternalApiSettings
    {
        public string ApiEndpoint { get; set; }
    }
}
