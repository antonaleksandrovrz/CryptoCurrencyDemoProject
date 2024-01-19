using CryptoCurrencyDemoProject.Data.Interfaces;

namespace CryptoCurrencyDemoProject.Data.Settings
{
    public class ExternalApiSettings : IExternalApiSettings
    {
        public string ApiEndpoint { get; set; }
    }
}
