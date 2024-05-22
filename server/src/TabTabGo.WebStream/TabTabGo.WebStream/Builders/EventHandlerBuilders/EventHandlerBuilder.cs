using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.Services.EventHandlers;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class EventHandlerBuilder
    {
        Func<IServiceProvider, IReceiveEvent> Object = null; 
        public EventHandlerBuilder IgnoreAllEvents()
        {
            var nullresult = new NullReceiveEvent();
            Object = (s) => nullresult;
            return this;
        }
        /// <summary>
        /// use same object
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public EventHandlerBuilder UseEventHandler(IReceiveEvent handler)
        {
            Object = (s) => handler;
            return this;
        }
        /// <summary>
        /// build object on each call
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public EventHandlerBuilder UseEventHandler(Func<IServiceProvider,IReceiveEvent> action)
        {
            Object = action;
            return this;
        }
        public EventHandlerBuilder UseEventHandler<T>() where T : IReceiveEvent
        {
            Object = s => s.GetRequiredService<T>();
            return this;
        }
        public EventHandlerBuilder UseFirstPassHandler(Action<FirstPassEventHandlerBuilder> action)
        {
            Object = (s) =>
            {
                FirstPassEventHandlerBuilder builder = new FirstPassEventHandlerBuilder();
                action(builder);
                return builder.Build(s);
            };
            return this;
        }

        public EventHandlerBuilder UseAllPassedHandlers(Action<AllPassEventHandlersBuilder> action)
        {
            Object = (s) =>
            {
                AllPassEventHandlersBuilder builder = new AllPassEventHandlersBuilder();
                action(builder);
                return builder.Build(s);
            };
            return this;
        }

        public EventHandlerBuilder UseEventHandlers(Action<EventHandlerListBuilders> action)
        {
            Object = (s) =>
            {
                EventHandlerListBuilders builder = new EventHandlerListBuilders();
                action(builder);
                return builder.Build(s);
            };
            return this;
        }
        public IReceiveEvent Build(IServiceProvider serviceProvider)
        {
            return Object(serviceProvider);
        }
    }
}
