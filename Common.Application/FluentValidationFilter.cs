using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application
{
    public class FluentValidationFilter<T> : IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly IValidator<T> _validator;

        public FluentValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateScope("FluentValidation");
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {

            var failures = (await _validator.ValidateAsync(context.Message)).Errors;


            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            await next.Send(context);
        }
    }
    
    public class FluentValidationConsumerConfigurationObserver :
   IConsumerConfigurationObserver
    {
        private readonly IServiceProvider _provider;

        public FluentValidationConsumerConfigurationObserver(IServiceProvider provider)
        {
            _provider = provider;
            ConsumerTypes = new HashSet<Type>();
            MessageTypes = new HashSet<Tuple<Type, Type>>();
        }

        public HashSet<Type> ConsumerTypes { get; }

        public HashSet<Tuple<Type, Type>> MessageTypes { get; }

        void IConsumerConfigurationObserver.ConsumerConfigured<TConsumer>(IConsumerConfigurator<TConsumer> configurator)
        {
            ConsumerTypes.Add(typeof(TConsumer));
        }

        void IConsumerConfigurationObserver.ConsumerMessageConfigured<TConsumer, TMessage>(IConsumerMessageConfigurator<TConsumer, TMessage> configurator)
        {
            MessageTypes.Add(Tuple.Create(typeof(TConsumer), typeof(TMessage)));

            var validator = _provider.GetService<IValidator<TMessage>>();
            if (validator == null) return;
            var filter = new FluentValidationFilter<TMessage>(validator);
            configurator.Message(m => m.UseFilter(filter));
        }
    }



}