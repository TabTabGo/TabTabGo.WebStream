using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Services
{
    public class EventHandlerList: IReceiveEvent
    {
        List<EventHandlerBuilder> _handlers;
        public EventHandlerList(List<EventHandlerBuilder> handlers)
        {
            _handlers = handlers ?? new List<EventHandlerBuilder>();
        }
        public Task OnEventReceived(string connectionId, WebStreamMessage message)
        { 
            foreach (var handler in _handlers)
            {
                handler.Build().OnEventReceived(connectionId, message);
            }
            return Task.CompletedTask;
        }
    }
}
