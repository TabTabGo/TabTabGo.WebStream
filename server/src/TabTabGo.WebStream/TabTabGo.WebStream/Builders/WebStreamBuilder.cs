using Services;
using System;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Services;
namespace TabTabGo.WebStream.Builders
{
    public class WebStreamBuilder
    {
        Action<EventHandlerBuilder> _EventHandlerBuilder;
        Func<IPushEvent> pushBuilder;
        public WebStreamBuilder UseEventHandler(Action<EventHandlerBuilder> action)
        {
            _EventHandlerBuilder = action;
            return this;
        }
        public WebStreamBuilder SetEventSender(Func<IPushEvent> func)
        {
            pushBuilder = func;
            return this;
        }
        public (IPushEvent, IReceiveEvent) Build()
        { 
            return (this.BuildEventSender(), this.BuildEventHandler());
        }

        public  IReceiveEvent BuildEventHandler()
        {
            var eventHandlerBuilder = new EventHandlerBuilder();
            _EventHandlerBuilder(eventHandlerBuilder);
            var eventHandler = eventHandlerBuilder.Build();
            if (eventHandler == null)
            {
                eventHandler = new NullReceiveEvent();
            }
            return  eventHandler;
        }

        public IPushEvent BuildEventSender()
        {
            return pushBuilder();
        }




    }
}
