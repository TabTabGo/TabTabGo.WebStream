using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlersServices
{
    public class FirstPassedEventHandler(
        List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers,
        IServiceProvider provider)
        : IReceiveEvent
    {
        List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> _handlers = handlers ?? new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();

        public Task OnEventReceived(string userId, WebStreamMessage message)
        {
            var handlerBuilder = _handlers.FirstOrDefault(s => s.Item1(message));
            if (handlerBuilder != default)
            {
                handlerBuilder.Item2.Build(provider).OnEventReceived(userId, message);
            }
            return Task.CompletedTask;
        }
    }
}
