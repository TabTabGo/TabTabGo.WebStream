using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            if (Object != null) throw new Exception("Please set up this handler Once");
            Object = (s) => handler;
            return this;
        }
        /// <summary>
        /// build object on each call
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public EventHandlerBuilder UseEventHandler(Func<IServiceProvider, IReceiveEvent> action)
        {
            if (Object != null) throw new Exception("Please set up this handler Once");

            Object = action;
            return this;
        }
        public EventHandlerBuilder UseEventHandler<T>() where T : IReceiveEvent
        {
            if (Object != null) throw new Exception("Please set up this handler Once"); 
            Object = s => s.GetRequiredService<T>();
            return this;
        }
        public EventHandlerBuilder UseFirstPassHandler(Action<FirstPassEventHandlerBuilder> action)
        {
            if (Object != null) throw new Exception("Please set up this handler Once");

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
            if (Object != null) throw new Exception("Please set up this handler Once");

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
            if (Object != null) throw new Exception("Please set up this handler Once"); 
            Object = (s) =>
            {
                EventHandlerListBuilders builder = new EventHandlerListBuilders();
                action(builder);
                return builder.Build(s);
            };
            return this;
        }
        bool logEnabled = false;


        /// <summary>
        /// log message of the this configured handler
        /// </summary> 
        public EventHandlerBuilder LogAllRecevedMessages()
        {
            if (Object == null) throw new Exception("please set up this handler before enable logging"); 
            if (!logEnabled)
            {
                logEnabled = true;
                var oldEvent = Object;
                Object = (s) =>
                {
                    EventHandlerListBuilders builder = new EventHandlerListBuilders();
                    builder.AddEventHandler(x => x.UseEventHandler(new LogRecevedEvents(s.GetRequiredService<ILogger<IReceiveEvent>>())));
                    builder.AddEventHandler(x => oldEvent(s));
                    return builder.Build(s);
                };
            }
            return this;
        }


        public IReceiveEvent Build(IServiceProvider serviceProvider)
        {
            return Object(serviceProvider);
        }
    }
}
