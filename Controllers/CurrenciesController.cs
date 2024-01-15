using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Models;
using CryptoCurrencyDemoProject.Data.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoCurrencyDemoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesService currenciesService;

        public CurrenciesController(ICurrenciesService currenciesService)
        {
            this.currenciesService = currenciesService;
        }

        // GET: api/<CurrenciesController>
        [HttpGet]
        public async Task<ActionResult<List<CurrencyModel>>> SelectAll()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await currenciesService.SelectAll();

                return cryptocurrencies == null || cryptocurrencies.Count == 0 ? (ActionResult<List<CurrencyModel>>)NotFound("No cryptocurrencies found") : (ActionResult<List<CurrencyModel>>)Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("externalApi")]
        public async Task<ActionResult<List<CurrencyModel>>> GetAsync()
        {
            try
            {
                ExternalApiService cryptoCurrenciesService = new(HttpContext.RequestServices.GetRequiredService<HttpClient>());
                List<CurrencyModel> cryptocurrencies = await cryptoCurrenciesService.GetCryptocurrenciesAsync();

                return cryptocurrencies == null || cryptocurrencies.Count == 0 ? (ActionResult<List<CurrencyModel>>)NotFound("No cryptocurrencies found") : (ActionResult<List<CurrencyModel>>)Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("trending")]
        public async Task<ActionResult<List<CurrencyModel>>> SelectTrendingCryptocurrencie()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await currenciesService.GetTrendingCryptocurrenciesAsync();

                return cryptocurrencies == null || cryptocurrencies.Count == 0 ? (ActionResult<List<CurrencyModel>>)NotFound("No cryptocurrencies found") : (ActionResult<List<CurrencyModel>>)Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("volumeLeaders")]
        public async Task<ActionResult<List<CurrencyModel>>> SelectVolumeLeadersCryptocurrencie()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await currenciesService.GetVolumeLeadersCryptocurrenciesAsync();

                return cryptocurrencies == null || cryptocurrencies.Count == 0 ? (ActionResult<List<CurrencyModel>>)NotFound("No cryptocurrencies found") : (ActionResult<List<CurrencyModel>>)Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<CurrencyModel>>> SelectPage(int page)
        {
            try
            {
                PaginationResponse<CurrencyModel> cryptocurrencies = await currenciesService.SelectPage(page);

                return cryptocurrencies == null || cryptocurrencies.Data.Count == 0
                    ? (ActionResult<List<CurrencyModel>>)NotFound("No cryptocurrencies found")
                    : (ActionResult<List<CurrencyModel>>)Ok(new PaginationResponse<CurrencyModel>
                    {
                        Data = cryptocurrencies.Data,
                        TotalPages = cryptocurrencies.TotalPages
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        
        // PUT api/<CurrenciesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] CurrencyModel cryptocurrencyModel)
        {
            try
            {
                CurrencyModel existingCurrency = currenciesService.Get(id);
                if (existingCurrency == null)
                {
                    return NotFound($"CurrencyModel with id = {id} not found");
                }

                currenciesService.Update(id, cryptocurrencyModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT api/<CurrenciesController>/5
        [HttpPut]
        public async Task<ActionResult> UpdateFromAPIAsync()
        {
            try
            {
                ExternalApiService cryptoCurrenciesService = new(HttpContext.RequestServices.GetRequiredService<HttpClient>());
                List<CurrencyModel> cryptocurrencies = await cryptoCurrenciesService.GetCryptocurrenciesAsync();

                if (cryptocurrencies == null || cryptocurrencies.Count == 0)
                {
                    return NotFound("No cryptocurrencies found");
                }

                _ = await currenciesService.InsertManyAsync(cryptocurrencies);
                return Ok("The database is updated from the external API");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE api/<CurrenciesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                CurrencyModel existingCurrency = currenciesService.Get(id);
                if (existingCurrency == null)
                {
                    return NotFound($"CurrencyModel with id = {id} not found");
                }

                currenciesService.Remove(id);
                return Ok($"CurrencyModel with id = {id} is deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
