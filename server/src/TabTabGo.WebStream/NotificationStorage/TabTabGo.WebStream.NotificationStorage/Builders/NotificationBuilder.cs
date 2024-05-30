using System;
using System.Collections.Generic;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.Services.EventHandlers;
namespace TabTabGo.WebStream.Builders
{
    internal abstract class NotificationBuilderService
    {
        internal abstract object Get(IServiceProvider serviceProvider);
        internal abstract Type GetTypeToRegest();
    }
    internal class NotificationBuilderService<T, Imp> : NotificationBuilderService where Imp:T
    {
        internal Func<IServiceProvider, Imp> Function;
        internal override object Get(IServiceProvider serviceProvider)
        {
            return Function(serviceProvider);
        }
        internal override Type GetTypeToRegest() { return typeof(T); }
    }
    public class NotificationBuilder
    {

        Action<WebStreamBuilder> _webStreamBuilderAction; 
        private readonly List<NotificationBuilderService> services = new List<NotificationBuilderService>(); 
        internal List<NotificationBuilderService> GetRegistedServices()
        {
            return services;
        } 
        public NotificationBuilder RegisteService<T, Implement>(Func<IServiceProvider,Implement> func) where Implement : T
        {
            services.Add(new NotificationBuilderService<T, Implement> { Function = func });
            return this;
        } 
        public NotificationBuilder SetupWebStream(Action<WebStreamBuilder> action)
        {
            _webStreamBuilderAction = action;
            return this;
        } 
        public WebStreamBuilder BuildWebStreamBuilder()
        {
            var webStreamBuilder = new WebStreamBuilder();
            _webStreamBuilderAction(webStreamBuilder);
            return webStreamBuilder;
        }
        public Action<WebStreamBuilder> GetWebStreamBuilderAction()
        {
            return _webStreamBuilderAction;
        }
    }
}
