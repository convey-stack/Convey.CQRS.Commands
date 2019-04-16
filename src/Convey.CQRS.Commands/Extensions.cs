using System;
using System.Threading.Tasks;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace Convey.CQRS.Commands
{
    public static class Extensions
    {
        public static Task SendAsync<TCommand>(this IBusPublisher busPublisher, TCommand command, ICorrelationContext context) 
            where TCommand : ICommand
            => busPublisher.PublishAsync(command, context);

        public static IBusSubscriber SubscribeCommand<TCommand>(this IBusSubscriber busSubscriber, 
            string @namespace = null, string queueName = null, Func<TCommand, ConveyException, IMessage> onError = null) 
            where TCommand : ICommand
        {
            busSubscriber.SubscribeMessage<TCommand>(async (sp, command, ctx) =>
            {
                var commandHandler = sp.GetService<ICommandHandler<TCommand>>();
                await commandHandler.HandleAsync(command, ctx);
            }, @namespace, queueName, onError);

            return busSubscriber;
        }
    }
}