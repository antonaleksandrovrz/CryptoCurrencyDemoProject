using CryptoCurrencyDemoProjectTest.Data.Interfaces;

namespace CryptoCurrencyDemoProjectTest.Data.Settings
{
    public class AuthSettings : IAuthSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audiance { get; set; }
    }
}
