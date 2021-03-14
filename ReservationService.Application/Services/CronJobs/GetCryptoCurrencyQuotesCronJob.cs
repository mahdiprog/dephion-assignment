using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace CryptoRate.Application.Services.CronJobs
{
    public class GetCryptoCurrencyQuotesCronJob: CronJobService
    {
        private readonly ILogger<GetCryptoCurrencyQuotesCronJob> _logger;
        private readonly ICryptoCurrencyService _cryptoCurrencyService;

        public GetCryptoCurrencyQuotesCronJob(IScheduleConfig<GetCryptoCurrencyQuotesCronJob> config,
            ILogger<GetCryptoCurrencyQuotesCronJob> logger,
            ICryptoCurrencyService cryptoCurrencyService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _cryptoCurrencyService = cryptoCurrencyService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCryptoCurrencyQuotesCronJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            var currencies = await _cryptoCurrencyService.GetAllCurrencies();
           await _cryptoCurrencyService.FetchLatestQuoteAndSetCache(currencies.Select(c => c.Symbol).ToArray());
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCryptoCurrencyQuotesCronJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}