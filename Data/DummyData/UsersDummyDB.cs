using CryptoCurrencyDemoProject.Data.Models;

namespace CryptoCurrencyDemoProject.Data.DummyData
{
    public class UsersDummyDB
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Username = "admin", Email = "admin@gmail.com", Password = "1234", Name = "Jon", Surname = "Doe", Role = "Administrator" },
            new UserModel() { Username = "guest", Email = "guest@gmail.com", Password = "1234", Name = "Tony", Surname = "Naika", Role = "Guest" },
        };
    }
}
