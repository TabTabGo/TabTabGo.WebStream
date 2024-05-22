using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlers
{
    public class EventHandlerList : IReceiveEvent
    {
        List<EventHandlerBuilder> _handlers;
        IServiceProvider _provider;
        public EventHandlerList(List<EventHandlerBuilder> handlers, IServiceProvider serviceProvider)
        {
            _handlers = handlers ?? new List<EventHandlerBuilder>();
            _provider = serviceProvider;
        }
        public Task OnEventReceived(string connectionId, WebStreamMessage message)
        {
            foreach (var handler in _handlers)
            {
                handler.Build(_provider).OnEventReceived(connectionId, message);
            }
            return Task.CompletedTask;
        }
    }
}
