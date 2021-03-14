using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CryptoRate.Application.Interfaces;
using CryptoRate.Application.Services;
using CryptoRate.Application.Services.CronJobs;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoRate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<ICryptoCurrencyService, CryptoCurrencyService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
            services.AddHttpClient<ICryptoCurrencyService, CryptoCurrencyService>(options =>
            {
                options.BaseAddress = new Uri(configuration["CoinMarketCap:BaseUrl"]);
                options.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration["CoinMarketCap:ApiKey"]);
            });
            services.AddHttpClient<IExchangeRateService, ExchangeRateService>(options =>
            {
                options.BaseAddress = new Uri(configuration["ExchangeRateApi:BaseUrl"]);
            });
            // Add Cron jobs
            services.AddCronJob<GetCryptoCurrencyQuotesCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = configuration["CoinMarketCap:CronJobExpression"];
            });
            services.AddCronJob<GetExchangeRatesCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                c.CronExpression = configuration["ExchangeRateApi:CronJobExpression"];
            });
            return services;
        }
    }
}
