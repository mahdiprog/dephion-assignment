using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoRate.Application.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ExchangeRateService> _logger;
        private readonly TimeSpan _slidingExpiration;

        public ExchangeRateService(HttpClient httpClient, IMemoryCache cache, IConfiguration configuration,
            ILogger<ExchangeRateService> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
            _slidingExpiration = TimeSpan.Parse(configuration["ExchangeRateApi:CacheTimeout"]);
        }
        /// <summary>
        /// Get exchange rate from the API and cache it
        /// </summary>
        /// <param name="symbols">currency symbols comma separated</param>
        /// <param name="baseCurrencySymbol">base currency that others will be compared to</param>
        /// <returns></returns>
        public async Task<ExchangeRate> GetLatestRate(string symbols, string baseCurrencySymbol = "USD")
        {
            var cacheKey = "ExchangeRates";
            var quote = await _cache.GetOrCreateAsync(cacheKey,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = _slidingExpiration;
                    return Task.Run(() => RequestLatestRate(symbols, baseCurrencySymbol));
                }).ConfigureAwait(false);
            return quote;
        }

        private async Task<ExchangeRate> RequestLatestRate(string symbols, string baseCurrencySymbol = "USD")
        {
            try
            {
                var jsonStream =
                    await _httpClient.GetStreamAsync($"/latest?base={baseCurrencySymbol}&symbols={symbols}");
                var exchangeRate = await JsonSerializer.DeserializeAsync<ExchangeRate>(jsonStream);
                return exchangeRate;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on getting rates from ExchangeRateApi");
                throw;
            }
        }

    }
}
