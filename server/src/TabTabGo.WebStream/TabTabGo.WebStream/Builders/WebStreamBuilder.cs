using System;
using System.Collections.Generic;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.Services.EventHandlers;
namespace TabTabGo.WebStream.Builders
{
    internal abstract class WebStreamBuilderService
    {
        internal abstract object Get(IServiceProvider serviceProvider);
        internal abstract Type GetTypeToRegest();
    }
    internal class WebStreamBuilderService<T, Imp> : WebStreamBuilderService where Imp:T
    {
        internal Func<IServiceProvider, Imp> Function;
        internal override object Get(IServiceProvider serviceProvider)
        {
            return Function(serviceProvider);
        }
        internal override Type GetTypeToRegest() { return typeof(T); }
    }
    public class WebStreamBuilder
    {

        Action<EventHandlerBuilder> _eventHandlerBuilder;
        Action<PushEventBuilders.PushEventBuilder> _pushEventBuilder;

        private readonly List<Type> _eventHandlerTypes = new List<Type>();
        private readonly List<WebStreamBuilderService> services = new List<WebStreamBuilderService>();

        internal List<Type> GetRegistedEventHandlers()
        {
            return _eventHandlerTypes;
        }
        internal List<WebStreamBuilderService> GetRegistedServices()
        {
            return services;
        }
        public WebStreamBuilder RegisteEventHandler<T>() where T : IReceiveEvent
        {
            _eventHandlerTypes.Add(typeof(T));
            return this;
        }
        public WebStreamBuilder RegisteService<T, Implement>(Func<IServiceProvider,Implement> func) where Implement : T
        {
            services.Add(new WebStreamBuilderService<T, Implement> { Function = func });
            return this;
        }
        public WebStreamBuilder SetupEventHandlers(Action<EventHandlerBuilder> action)
        {
            _eventHandlerBuilder = action;
            return this;
        }
        public WebStreamBuilder SetupIPushEvent(Action<PushEventBuilder> action)
        {
            _pushEventBuilder = action;
            return this;
        }
        public IReceiveEvent BuildEventHandler(IServiceProvider provider)
        {
            var eventHandlerBuilder = new EventHandlerBuilder();
            _eventHandlerBuilder(eventHandlerBuilder);
            var eventHandler = eventHandlerBuilder.Build(provider);
            return eventHandler ?? new NullReceiveEvent();
        }
        public IPushEvent BuildIPushEvent(IServiceProvider serviceProvider)
        {
            var pushBuilder = new PushEventBuilder();
            _pushEventBuilder(pushBuilder);
            return pushBuilder.Build(serviceProvider);
        }
    }
}
