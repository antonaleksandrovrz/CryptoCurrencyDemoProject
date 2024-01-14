using CryptoCurrencyDemoProject.Data.Interfaces;
using CryptoCurrencyDemoProject.Data.Models;
using CryptoCurrencyDemoProject.Services;
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
        public async Task<ActionResult<List<CryptocurrencyModel>>> SelectAll()
        {
            try
            {
                var cryptocurrencies = await currenciesService.SelectAll();

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

        [HttpGet]
        [Route("trending")]
        public async Task<ActionResult<List<CryptocurrencyModel>>> SelectTrendingCryptocurrencie()
        {
            try
            {
                var cryptocurrencies = await currenciesService.GetTrendingCryptocurrenciesAsync();

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

        [HttpGet]
        [Route("volumeLeaders")]
        public async Task<ActionResult<List<CryptocurrencyModel>>> SelectVolumeLeadersCryptocurrencie()
        {
            try
            {
                var cryptocurrencies = await currenciesService.GetVolumeLeadersCryptocurrenciesAsync();

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

        [HttpGet("{page}")]
        public async Task<ActionResult<List<CryptocurrencyModel>>> SelectPage(int page)
        {
            try
            {
                var cryptocurrencies = await currenciesService.SelectPage(page);

                if (cryptocurrencies == null || cryptocurrencies.Data.Count == 0)
                {
                    return NotFound("No cryptocurrencies found");
                }

                return Ok(new PaginationResponse<CryptocurrencyModel>
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

        //// GET api/<CurrenciesController>/5
        //[HttpGet("{id}")]
        //public ActionResult<CryptocurrencyModel> Get(string id)
        //{
        //    try
        //    {
        //        var currencyModel = currenciesService.Get(id);

        //        if (currencyModel == null)
        //        {
        //            return NotFound($"CurrencyModel with id = {id} not found");
        //        }
        //        return currencyModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500, "Internal server error: " + ex.Message);
        //    }
        //}

        //// POST api/<CurrenciesController>
        //[HttpPost]
        //public ActionResult<CryptocurrencyModel> Post([FromBody] CryptocurrencyModel cryptocurrencyModel)
        //{
        //    try
        //    {
        //        currenciesService.Insert(cryptocurrencyModel);
        //        return CreatedAtAction(nameof(Get), new { id = cryptocurrencyModel.Id }, cryptocurrencyModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(500, "Internal server error: " + ex.Message);
        //    }
        //}

        // PUT api/<CurrenciesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] CryptocurrencyModel cryptocurrencyModel)
        {
            try
            {
                var existingCurrency = currenciesService.Get(id);
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
                CryptoCurrenciesService cryptoCurrenciesService = new CryptoCurrenciesService(HttpContext.RequestServices.GetRequiredService<HttpClient>());
                var cryptocurrencies = await cryptoCurrenciesService.GetCryptocurrenciesAsync();

                if (cryptocurrencies == null || cryptocurrencies.Count == 0)
                {
                    return NotFound("No cryptocurrencies found");
                }

                await currenciesService.InsertManyAsync(cryptocurrencies);
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
                var existingCurrency = currenciesService.Get(id);
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
