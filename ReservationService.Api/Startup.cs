using System.Reflection;
using AutoMapper;
using Common.Application.Extensions;
using Common.Web;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReservationService.Application;
using ReservationService.Infra;
using ReservationService.Infra.Persistence;
using ReservationService.Messaging.Meetings;
using Swashbuckle.AspNetCore.Swagger;

namespace ReservationService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration);
            services.AddApplication();

            services.AddControllers();
            services.AddValidatorsFromAssemblyContaining<MeetingAddCommandValidator>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservationApi", Version = "v1" });
                c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,

                    Type = SecuritySchemeType.ApiKey
                });
                c.AddFluentValidationRules();

                c.CustomSchemaIds(i => i.FullName);
            });
            services.AddSingleton(Configuration);
            services.ConfigureBus(Configuration, "ReservationApi", Assembly.GetExecutingAssembly());
            services.AddHealthChecks()
                .AddDbContextCheck<ReservationDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }
            app.UseCustomExceptionHandler();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<ReservationDbContext>().Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
