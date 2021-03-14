using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CryptoRate.Application.Interfaces;
using CryptoRate.Domain.Models;
using MediatR;

namespace CryptoRate.Application.CryptoCurrencies
{
    public class GetAllCryptoCurrenciesQuery: IRequest<IEnumerable<CryptoCurrency>>
    {
       
    }
    public class GetAllCryptoCurrenciesQueryHandler : IRequestHandler<GetAllCryptoCurrenciesQuery, IEnumerable<CryptoCurrency>>
    {
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        public GetAllCryptoCurrenciesQueryHandler(ICryptoCurrencyService cryptoCurrencyService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
        }


        public async Task<IEnumerable<CryptoCurrency>> Handle(GetAllCryptoCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return await _cryptoCurrencyService.GetAllCurrencies().ConfigureAwait(false);
        }
    }
}
