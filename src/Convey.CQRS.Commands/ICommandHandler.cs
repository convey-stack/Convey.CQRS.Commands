using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;

namespace Convey.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}