using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ReservationService.Api;
using ReservationService.IntegrationTests.Common;

namespace ReservationService.IntegrationTests
{
    public class TestingWebAppFactory<T> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                

                services.AddAntiforgery(t =>
                {
                    t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                    t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
                });
                
            });
        }
    }
}