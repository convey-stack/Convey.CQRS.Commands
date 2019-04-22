using System.Threading.Tasks;

namespace Convey.CQRS.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : class, ICommand;
    }
}