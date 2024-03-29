﻿using CryptoCurrencyDemoProjectTest.Data.Interfaces;
using CryptoCurrencyDemoProjectTest.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoCurrencyDemoProjectTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrenciesService currenciesService;
        private readonly IExternalApiService externalApiService;

        public CurrenciesController(ICurrenciesService currenciesService,IExternalApiService externalApiService)
        {
            this.currenciesService = currenciesService;
            this.externalApiService = externalApiService;
        }

        // GET: api/<CurrenciesController>
        [HttpGet]
        public async Task<ActionResult<List<CurrencyModel>>> SelectAll()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await currenciesService.SelectAll();
                return Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<CurrenciesController>/externalApi
        [Authorize(Roles = "Administrator")]
        [HttpGet("Admins")]
        [Route("externalApi")]
        public async Task<ActionResult<List<CurrencyModel>>> GetAsync()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await externalApiService.GetCryptocurrenciesAsync();
                return Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT api/<CurrenciesController>/
        [HttpPut]
        public async Task<ActionResult> UpdateFromAPIAsync()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await externalApiService.GetCryptocurrenciesAsync();
                if(await currenciesService.InsertManyAsync(cryptocurrencies))
                {
                    return Ok(cryptocurrencies);
                }

                throw new Exception("Failed to update the database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<CurrenciesController>/trending
        [Authorize(Roles = "Guest")]
        [HttpGet]
        [Route("trending")]
        public async Task<ActionResult<List<CurrencyModel>>> SelectTrendingCryptocurrencie()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await currenciesService.GetTrendingCryptocurrenciesAsync();
                return Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<CurrenciesController>/volumeLeaders
        [HttpGet]
        [Route("volumeLeaders")]
        public async Task<ActionResult<List<CurrencyModel>>> SelectVolumeLeadersCryptocurrencie()
        {
            try
            {
                List<CurrencyModel> cryptocurrencies = await currenciesService.GetVolumeLeadersCryptocurrenciesAsync();
                return Ok(cryptocurrencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/<CurrenciesController>/5
        [HttpGet("{page}")]
        public async Task<ActionResult<List<CurrencyModel>>> SelectPage(int page)
        {
            try
            {
                PaginationResponse<CurrencyModel> cryptocurrencies = await currenciesService.SelectPage(page);
                return Ok(new PaginationResponse<CurrencyModel>
                {
                    Data = cryptocurrencies.Data,
                    TotalPages = cryptocurrencies.TotalPages
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // PUT api/<CurrenciesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] CurrencyModel cryptocurrencyModel)
        {
            try
            {
                currenciesService.Update(id, cryptocurrencyModel);
                return Ok($"CurrencyModel with id = {id} is updated.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // DELETE api/<CurrenciesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                currenciesService.Remove(id);
                return Ok($"CurrencyModel with id = {id} is deleted.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
