using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlers
{
    public class AllPassedEventHandlers : IReceiveEvent
    {
        private readonly List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> _handlers;
        private readonly IServiceProvider _serviceProvider;
        public AllPassedEventHandlers(List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers, IServiceProvider serviceProvider)
        {
            _handlers = handlers ?? new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();
            _serviceProvider = serviceProvider;
        }
        public Task OnEventReceived(string connectionId, WebStreamMessage message)
        {
            var handlerBuilder = _handlers.Where(s => s.Item1(message)).ToList();
            foreach (var handler in handlerBuilder)
            {
                handler.Item2.Build(_serviceProvider).OnEventReceived(connectionId, message);
            }
            return Task.CompletedTask;
        }
    }
}
