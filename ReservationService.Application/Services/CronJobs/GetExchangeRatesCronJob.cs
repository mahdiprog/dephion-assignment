using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoRate.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoRate.Application.Services.CronJobs
{
    public class GetExchangeRatesCronJob: CronJobService
    {
        private readonly ILogger<GetExchangeRatesCronJob> _logger;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IConfiguration _configuration;

        public GetExchangeRatesCronJob(IScheduleConfig<GetExchangeRatesCronJob> config,
            ILogger<GetExchangeRatesCronJob> logger,
            IExchangeRateService exchangeRateService,
            IConfiguration configuration)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetExchangeRatesCronJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            IConfigurationSection currencySection = _configuration.GetSection("AvailableCurrencies");
            var symbols = currencySection.AsEnumerable()
                .Where(c => !string.IsNullOrEmpty(c.Value))
                .Select(c => c.Value);
            
            return _exchangeRateService.GetLatestRate(string.Join(",", symbols));
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetExchangeRatesCronJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}