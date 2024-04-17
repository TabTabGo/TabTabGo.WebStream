using Services;
using System;
using TabTabGo.WebStream.Services;

namespace TabTabGo.WebStream.Builders.EventHandlerBuilders
{
    public class EventHandlerBuilder
    {
        Func<IReceiveEvent> Object = null;
        public EventHandlerBuilder IgnoreAllEvents()
        {
            var nullresult = new NullReceiveEvent();
            Object = () => nullresult;
            return this;
        }
        /// <summary>
        /// use same object
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public EventHandlerBuilder SetEventHandler(IReceiveEvent handler)
        {
            Object = () => handler;
            return this;
        }
        /// <summary>
        /// build object on each call
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public EventHandlerBuilder SetEventHandler(Func<IReceiveEvent> action)
        {
            Object = action;
            return this;
        }
        public EventHandlerBuilder UseComplexHandler(Action<ComplexEventHandlerBuilder> action)
        {
            Object = () =>
            {
                ComplexEventHandlerBuilder builder = new ComplexEventHandlerBuilder();
                action(builder);
                return builder.Build();
            };
            return this;
        }
        public IReceiveEvent Build()
        {
            return Object();
        }
    }
}
