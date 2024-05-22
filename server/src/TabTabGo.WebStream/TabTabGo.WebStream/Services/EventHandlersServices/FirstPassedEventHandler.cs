using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlers
{
    public class FirstPassedEventHandler : IReceiveEvent
    {
        List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> _handlers;
        IServiceProvider _serviceProvider;
        public FirstPassedEventHandler(List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers, IServiceProvider provider)
        {
            _handlers = handlers ?? new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();
            _serviceProvider = provider;
        }
        public Task OnEventReceived(string connectionId, WebStreamMessage message)
        {
            var handlerBuilder = _handlers.Where(s => s.Item1(message)).FirstOrDefault();
            if (handlerBuilder != default)
            {
                handlerBuilder.Item2.Build(_serviceProvider).OnEventReceived(connectionId, message);
            }
            return Task.CompletedTask;
        }
    }
}
