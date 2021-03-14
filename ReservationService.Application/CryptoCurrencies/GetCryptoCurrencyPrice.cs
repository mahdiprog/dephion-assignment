using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.ViewModels;
using CryptoRate.Domain.Models;
using MediatR;

namespace CryptoRate.Application.CryptoCurrencies
{
    public class GetCryptoCurrencyPriceQuery: IRequest<decimal>
    {
        public string Symbol { get; set; }
    }
    public class GetCryptoCurrencyPriceQueryHandler : IRequestHandler<GetCryptoCurrencyPriceQuery, decimal>
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        public GetCryptoCurrencyPriceQueryHandler(ICryptoCurrencyService cryptoCurrencyService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
        }


        public async Task<decimal> Handle(GetCryptoCurrencyPriceQuery request, CancellationToken cancellationToken)
        {
            return await _cryptoCurrencyService.GetLatestPrice(request.Symbol).ConfigureAwait(false);
        }
    }
}
