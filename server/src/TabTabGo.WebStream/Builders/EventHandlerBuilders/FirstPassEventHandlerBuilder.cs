using System;
using System.Collections.Generic;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.EventHandlersServices;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class FirstPassEventHandlerBuilder
    {
        private readonly List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers = new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();
        public FirstPassEventHandlerBuilder AddEventHandler(Func<WebStreamMessage, bool> predict, Action<EventHandlerBuilder> action)
        {
            var builder = new EventHandlerBuilder();
            action(builder);
            var item = (predict, builder);
            handlers.Add(item);
            return this;
        }
        /// <summary>
        /// check event name
        /// </summary> 
        public FirstPassEventHandlerBuilder AddEventHandler(string eventName, Action<EventHandlerBuilder> action)
        {
            bool predict(WebStreamMessage s) => s.EventName.Equals(eventName);
            return this.AddEventHandler(predict, action);
        }
        public FirstPassedEventHandler Build(IServiceProvider serviceProvider)
        {
            return new FirstPassedEventHandler(handlers, serviceProvider);
        }
    }
}
