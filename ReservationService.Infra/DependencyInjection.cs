using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservationService.Application.Interfaces;
using ReservationService.Infra.Persistence;

namespace ReservationService.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<ReservationDbContext>(options =>
                options.UseInMemoryDatabase("ReservationDbContext"));
                //options.UseSqlServer(configuration.GetConnectionString("ReservationDbContext")));

                services.AddScoped<IReservationDbContext>(provider => provider.GetService<ReservationDbContext>());
            return services;
        }
    }

}
