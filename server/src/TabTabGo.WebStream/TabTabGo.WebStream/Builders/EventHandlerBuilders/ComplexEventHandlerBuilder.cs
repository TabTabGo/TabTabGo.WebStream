using System;
using System.Collections.Generic;
using System.Text;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class ComplexEventHandlerBuilder
    { 
        List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers = new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();
        public ComplexEventHandlerBuilder AddEventHandler(Func<WebStreamMessage, bool> predict, Action<EventHandlerBuilder> action)
        {
            var builder = new EventHandlerBuilder();
            action(builder);
            var item = (predict, builder);
            handlers.Add(item);
            return this;
        }
        public ComplexEventHandler Build()
        {
            return new ComplexEventHandler(handlers);
        }
    }
}
