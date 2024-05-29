using System;
using System.Collections.Generic;
using TabTabGo.WebStream.Services.EventHandlers;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class EventHandlerListBuilders
    {

        private readonly List<EventHandlerBuilder> handlers = new List<EventHandlerBuilder>();
        public EventHandlerListBuilders AddEventHandler(Action<EventHandlerBuilder> action)
        {
            var builder = new EventHandlerBuilder();
            action(builder);
            handlers.Add(builder);
            return this;
        }
        public EventHandlerList Build(IServiceProvider serviceProvider)
        {
            return new EventHandlerList(handlers, serviceProvider);
        }
    }
}
