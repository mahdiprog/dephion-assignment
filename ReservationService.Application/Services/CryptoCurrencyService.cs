using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.ViewModels;
using CryptoRate.Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoRate.Application.Services
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _slidingExpiration;
        private readonly ILogger<CryptoCurrencyService> _logger;
        private const string PriceCacheKey = "{0}-Price";

        public CryptoCurrencyService(HttpClient httpClient, IMemoryCache cache, IConfiguration configuration,
            ILogger<CryptoCurrencyService> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
            _slidingExpiration = TimeSpan.Parse(configuration["CoinMarketCap:CacheTimeout"]);
        }
        /// <summary>
        /// Send request to the API and save result in cache
        /// </summary>
        /// <param name="symbols">crypto currency symbols</param>
        /// <returns>List all requested crypto currencies with their prices</returns>
        public async Task<IEnumerable<CryptoCurrencyWithPrice>> FetchLatestQuoteAndSetCache(params string[] symbols)
        {
            var quotes = (await RequestLatestQuotes(symbols)).ToList();
            
            // Save data in cache.
            foreach (var quote in quotes)
            {
                _cache.Set(string.Format(PriceCacheKey, quote.Symbol), quote.Quote.Usd.Price,
                    new MemoryCacheEntryOptions {SlidingExpiration = _slidingExpiration});
            }

            return quotes;
        }
        /// <summary>
        /// Read Crypto currency data and price from th API
        /// </summary>
        /// <param name="symbols"></param>
        /// <returns></returns>
        private async Task<IEnumerable<CryptoCurrencyWithPrice>> RequestLatestQuotes(params string[] symbols)
        {
            try
            {
                var jsonStream =
                    await _httpClient.GetStreamAsync(
                            $"/v1/cryptocurrency/quotes/latest?aux=is_active&symbol={string.Join(",", symbols)}")
                        .ConfigureAwait(false);
                var coinInfo = await CoinMarketCapResponse<CryptoCurrencyWithPrice>.FromJson(jsonStream)
                    .ConfigureAwait(false);
                if (coinInfo == null)
                {
                    _logger.LogError("Empty response on getting quotes from MarketCoinCap");
                    return null;
                }

                if (coinInfo.Status.ErrorCode != 0)
                {
                    _logger.LogError(coinInfo.Status.ErrorMessage);
                    return null;
                }

                return coinInfo.Data;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on getting quotes from MarketCoinCap");
            }

            return null;
        }

        private async Task<decimal> RequestLatestQuote(string symbol)
        {
            var quote = (await RequestLatestQuotes(symbol)).FirstOrDefault();
            return quote.Quote.Usd.Price;
        }
        /// <summary>
        /// Gets the latest price of specified symbol 
        /// </summary>
        /// <param name="symbol">crypto currency symbol</param>
        /// <returns>price</returns>
        public async Task<decimal> GetLatestPrice(string symbol)
        {
            var cacheKey = string.Format(PriceCacheKey, symbol);
            var quote = await _cache.GetOrCreateAsync(cacheKey,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = _slidingExpiration;
                    return Task.Run(() => RequestLatestQuote(symbol));
                }).ConfigureAwait(false);
            return quote;
        }
        /// <summary>
        /// get all crypto currency data (sampled 20) and cache it
        /// </summary>
        /// <returns>list of CryptoCurrency </returns>
        public async Task<IEnumerable<CryptoCurrency>> GetAllCurrencies()
        {
            var cacheKey = "symbols";

            var quote = await _cache.GetOrCreateAsync(cacheKey,
                cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromDays(1);
                    return Task.Run(RequestAllCurrencies);
                }).ConfigureAwait(false);
            return quote;
        }

        private async Task<IEnumerable<CryptoCurrency>> RequestAllCurrencies()
        {
            try
            {
                var response =
                    await _httpClient.GetFromJsonAsync<CoinMarketCapResponse<CryptoCurrency>>(
                        "/v1/cryptocurrency/map?limit=20&aux=").ConfigureAwait(false);
                if (response == null)
                {
                    _logger.LogError("Empty response on getting currency maps from MarketCoinCap");
                    return null;
                }

                if (response.Status.ErrorCode != 0)
                {
                    _logger.LogError(response.Status.ErrorMessage);
                    return null;
                }

                return response.Data;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on getting currency maps from MarketCoinCap");
            }

            return null;
        }
    }
}