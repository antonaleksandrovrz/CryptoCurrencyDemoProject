namespace CryptoCurrencyDemoProject.Data.Interfaces
{
    public interface IAuthSettings
    {
        string Key { get; set; }
        string Issuer { get; set; }
        string Audiance { get; set; }
    }
}
