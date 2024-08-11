using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlersServices
{
    public class AllPassedEventHandlers(
        List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers,
        IServiceProvider serviceProvider)
        : IReceiveEvent
    {
        private readonly List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> _handlers = handlers ?? new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();

        public Task OnEventReceived(UserIdData userId, WebStreamMessage message)
        {
            var handlerBuilder = _handlers.Where(s => s.Item1(message)).ToList();
            foreach (var handler in handlerBuilder)
            {
                handler.Item2.Build(serviceProvider).OnEventReceived(userId, message);
            }
            return Task.CompletedTask;
        }
    }
}
