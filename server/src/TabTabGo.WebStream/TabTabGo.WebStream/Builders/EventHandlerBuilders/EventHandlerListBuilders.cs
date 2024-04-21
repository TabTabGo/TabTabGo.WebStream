using System;
using System.Collections.Generic;
using System.Text;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class EventHandlerListBuilders
    {

        List<EventHandlerBuilder> handlers = new List<EventHandlerBuilder>();
        public EventHandlerListBuilders AddEventHandler(Action<EventHandlerBuilder> action)
        {
            var builder = new EventHandlerBuilder();
            action(builder); 
            handlers.Add(builder);
            return this;
        }
        public EventHandlerList Build()
        {
            return new EventHandlerList(handlers);
        }
    }
}
