using CryptoCurrencyDemoProject.Data.Models;
using CryptoCurrencyDemoProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrencyDemoProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoCurrenciesController : ControllerBase        
    {
        [HttpGet]
        public async Task<ActionResult<List<CryptocurrencyModel>>> GetAsync()
        {
            try
            {
                CryptoCurrenciesService cryptoCurrenciesService = new CryptoCurrenciesService(HttpContext.RequestServices.GetRequiredService<HttpClient>());
                var cryptocurrencies = await cryptoCurrenciesService.GetCryptocurrenciesAsync();

                if (cryptocurrencies == null || cryptocurrencies.Count == 0)
                {
                    return NotFound("No cryptocurrencies found");
                }

                return Ok(cryptocurrencies);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
