using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GreenPipes;

namespace Common.Application
{
    public class FluentValidationFilter<TContext> : IFilter<TContext>
    where TContext : class, PipeContext
    {

        public void Probe(ProbeContext context)
        {
            var scope = context.CreateFilterScope("FluentValidation");
        }

        public async Task Send(TContext context, IPipe<TContext> next)
        {
            if (context.TryGetPayload(out IEnumerable<IValidator<TContext>> validators))
            {
                var failures = validators
                    .Select(v => v.Validate(context))
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }

            }
            await next.Send(context);

        }
    }
    public class FluentValidationSpecification<TContext> : IPipeSpecification<TContext>
     where TContext : class, PipeContext
    {
        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public void Apply(IPipeBuilder<TContext> builder)
        {
            builder.AddFilter(new FluentValidationFilter<TContext>());
        }
    }

    public static class FluentValidationExtensionMethods
    {
        public static void UseFluentValidation<TContext>(this IPipeConfigurator<TContext> configurator)
            where TContext : class, PipeContext
        {
            configurator.AddPipeSpecification(new FluentValidationSpecification<TContext>());
        }
    }
}