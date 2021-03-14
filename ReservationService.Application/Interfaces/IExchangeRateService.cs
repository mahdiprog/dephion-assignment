using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoRate.Application.ViewModels;

namespace CryptoRate.Application.Interfaces
{
    public interface IExchangeRateService
    {
        Task<ExchangeRate> GetLatestRate(string symbols, string baseCurrencySymbol = "USD");
    }
}