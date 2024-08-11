using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlersServices
{
    public class EventHandlerList(List<EventHandlerBuilder> handlers, IServiceProvider serviceProvider)
        : IReceiveEvent
    {
        List<EventHandlerBuilder> _handlers = handlers ?? new List<EventHandlerBuilder>();

        public Task OnEventReceived(UserIdData userId, WebStreamMessage message)
        {
            foreach (var handler in _handlers)
            {
                handler.Build(serviceProvider).OnEventReceived(userId, message);
            }
            return Task.CompletedTask;
        }
    }
}
