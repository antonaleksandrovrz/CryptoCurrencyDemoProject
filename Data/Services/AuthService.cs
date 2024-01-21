using CryptoCurrencyDemoProjectTest.Data.DummyData;
using CryptoCurrencyDemoProjectTest.Data.Interfaces;
using CryptoCurrencyDemoProjectTest.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CryptoCurrencyDemoProjectTest.Data.Services
{
    public class AuthService : IAuthService
    {
        private IAuthSettings _authSettings;

        public AuthService(IAuthSettings authSettings)
        {
            _authSettings = authSettings;
        }

        public string Authenticate(UserLogin userLogin)
        {
            var currentUser = UsersDummyDB.Users.FirstOrDefault(o => o.Username.ToLower() == userLogin.Username.ToLower() && o.Password == userLogin.Password);

            if (currentUser != null)
            {
                return GenerateToken(currentUser);
            }

            else
            {
                throw new Exception("User not found");
            }
        }
        public string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_authSettings.Issuer,
              _authSettings.Audiance,
              claims,
              expires: DateTime.Now.AddMinutes(10),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}