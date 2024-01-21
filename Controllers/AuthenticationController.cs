using CryptoCurrencyDemoProject.Data.DummyData;
using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CryptoCurrencyDemoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthService _authorizationService;

        public AuthenticationController(IAuthService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult<string> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                string token = _authorizationService.Authenticate(userLogin);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return NotFound("User not found.");
            }
        }

        
    }
}
