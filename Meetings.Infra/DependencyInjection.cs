using Meetings.Infra.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Meetings.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("ReservationDbContext"));
                //options.UseSqlServer(configuration.GetConnectionString("ReservationDbContext")));
            return services;
        }
    }

}
