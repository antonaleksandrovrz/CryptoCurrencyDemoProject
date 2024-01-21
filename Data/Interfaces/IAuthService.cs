using CryptoCurrencyDemoProject.Data.DummyData;
using CryptoCurrencyDemoProject.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CryptoCurrencyDemoProject.Data.Interfaces
{
    public interface IAuthService
    {
        string Authenticate(UserLogin userLogin);
        string GenerateToken(UserModel user);
    }
}
