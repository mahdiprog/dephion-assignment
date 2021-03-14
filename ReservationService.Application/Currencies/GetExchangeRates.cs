using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.CryptoCurrencies;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.ViewModels;
using CryptoRate.Domain.Models;
using MediatR;

namespace CryptoRate.Application.Currencies
{
    public class GetExchangeRatesQuery: IRequest<ExchangeRate>
    {
        public IEnumerable<string> symbols { get; set; }
    }
    public class GetExchangeRatesQueryHandler : IRequestHandler<GetExchangeRatesQuery, ExchangeRate>
    {
        private readonly IExchangeRateService _exchangeRateService;

        public GetExchangeRatesQueryHandler(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }


        public async Task<ExchangeRate> Handle(GetExchangeRatesQuery request, CancellationToken cancellationToken)
        {
            return await _exchangeRateService.GetLatestRate(string.Join(",",request.symbols)).ConfigureAwait(false);
        }
    }
}
