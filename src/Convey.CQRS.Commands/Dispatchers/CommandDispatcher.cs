using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Convey.CQRS.Commands.Dispatchers
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task SendAsync<T>(T command) where T : class, ICommand
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var handler = scope.ServiceProvider.GetService<ICommandHandler<T>>();
                if (handler is null)
                {
                    throw new InvalidOperationException($"Command handler for: '{command}' was not found.");
                }
                
                return handler.HandleAsync(command);
            }
        }
    }
}