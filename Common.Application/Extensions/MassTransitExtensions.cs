using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Application.Messaging;
using GreenPipes.Internals.Extensions;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration.MultiBus;
using MassTransit.Metadata;
using MassTransit.MultiBus;
using MassTransit.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection ConfigureBus(this IServiceCollection services, IConfiguration configuration,
            string apiResourceName, Assembly assembly,
            Action<IServiceCollectionConfigurator<IExternalBus>> postAction = null)
        {
            var config = new MassTransitConfig();

            configuration.Bind("MassTransitConfig", config);
            // get referenced assemblies
            var assemblies = new List<Assembly> {assembly};

            assemblies.AddRange(assembly.GetProjectReferencedAssemblies());
            // find consumers
            var types = AssemblyTypeCache.FindTypes(assemblies, TypeMetadataCache.IsConsumerOrDefinition).GetAwaiter()
                .GetResult();
            var consumers = types.FindTypes(TypeClassification.Concrete | TypeClassification.Closed).ToArray();
            var internals = new List<Type>();
            var externals = new List<Type>();
            foreach (var type in consumers)
                if (type.HasInterface<IExternalConsumer>())
                {
                    externals.Add(type);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"{type.FullName} registered into External Bus");
                }

                else
                {
                    internals.Add(type);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"{type.FullName} registered into Mediator");
                }

            services.AddMediator(x =>
            {
                x.AddConsumers(internals.ToArray());
                x.ConfigureMediator((provider, cfg) => cfg.UseFluentValidation());
            });
            //services.AddMassTransit(x =>
            //{
            //    x.AddConsumers(internals.ToArray());
            //    x.AddMediator((provider, cfg) => { cfg.UseFluentValidation(); });
            //});


            services.AddMassTransit<IExternalBus>(x =>
            {
                x.AddConsumers(externals.ToArray());
                x.AddBus(provider =>
                    config.BusType switch
                    {
                        BusType.AzureServiceBus => throw new NotImplementedException(),
                        BusType.ActiveMQ => throw new NotImplementedException(),
                        BusType.AmazonSQS => throw new NotImplementedException(),

                        _ => Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            cfg.Host(new Uri(config.RabbitMQ.Address), h =>
                            {
                                h.Username(config.RabbitMQ.Username);
                                h.Password(config.RabbitMQ.Password);
                            });

                            cfg.ReceiveEndpoint(apiResourceName, ep => { ep.ConfigureConsumers(provider); });
                            cfg.UseFluentValidation();
                            
                        })
                    });

                postAction?.Invoke(x);
            });

            services.AddMassTransitHostedService();

            return services;
        }
        
    }
}