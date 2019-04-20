using Microsoft.Extensions.DependencyInjection;

namespace Convey.CQRS.Commands
{
    public static class Extensions
    {
        public static IConveyBuilder AddCommandHandlers(this IConveyBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromEntryAssembly()
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }
    }
}