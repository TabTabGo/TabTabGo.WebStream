using System;
using System.Collections.Generic;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.EventHandlers;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class AllPassEventHandlersBuilder
    {
       private readonly List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers = new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();
        public AllPassEventHandlersBuilder AddEventHandler(Func<WebStreamMessage, bool> predict, Action<EventHandlerBuilder> action)
        {
            var builder = new EventHandlerBuilder();
            action(builder);
            var item = (predict, builder);
            handlers.Add(item);
            return this;
        }

        public AllPassEventHandlersBuilder AddEventHandler(string eventName, Action<EventHandlerBuilder> action)
        {
            bool predict(WebStreamMessage s) => s.EventName.Equals(eventName);
            return this.AddEventHandler(predict, action);
        }


        public AllPassedEventHandlers Build(IServiceProvider serviceProvider)
        {
            return new AllPassedEventHandlers(handlers, serviceProvider);
        }
    }
}
