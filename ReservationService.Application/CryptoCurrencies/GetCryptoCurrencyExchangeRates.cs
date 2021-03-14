using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoRate.Application.Currencies;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.ViewModels;
using CryptoRate.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CryptoRate.Application.CryptoCurrencies
{
    public class GetCryptoCurrencyExchangeRatesQuery : IRequest<Dictionary<string, decimal>>
    {
        public string Symbol { get; set; }
    }

    public class
        GetCryptoCurrencyExchangeRatesQueryHandler : IRequestHandler<GetCryptoCurrencyExchangeRatesQuery,
            Dictionary<string, decimal>>
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;
        private readonly IConfiguration _configuration;
        private readonly IExchangeRateService _exchangeRateService;

        public GetCryptoCurrencyExchangeRatesQueryHandler(ICryptoCurrencyService cryptoCurrencyService,
            IConfiguration configuration, IExchangeRateService exchangeRateService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
            _configuration = configuration;
            _exchangeRateService = exchangeRateService;
        }


        public async Task<Dictionary<string, decimal>> Handle(GetCryptoCurrencyExchangeRatesQuery request,
            CancellationToken cancellationToken)
        {
            IConfigurationSection currencySection = _configuration.GetSection("AvailableCurrencies");
            var symbols = currencySection.AsEnumerable()
                .Where(c => !string.IsNullOrEmpty(c.Value))
                .Select(c => c.Value);

            var usdPrice = await _cryptoCurrencyService.GetLatestPrice(request.Symbol).ConfigureAwait(false);
            var exchangeRate = await _exchangeRateService.GetLatestRate(string.Join(",", symbols))
                .ConfigureAwait(false);

            var prices = new Dictionary<string, decimal>();
            // calculate price based on currency rate
            foreach (var symbol in symbols)
            {
                if (symbol.Equals("usd", StringComparison.InvariantCultureIgnoreCase))
                    prices.Add("USD", usdPrice);
                else
                {
                    var priceWithRate = usdPrice * exchangeRate.Rates[symbol];
                    prices.Add(symbol, priceWithRate);
                }
            }


            return prices;
        }
    }
}
