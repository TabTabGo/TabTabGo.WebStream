using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Services;

namespace TabTabGo.WebStream.Extensions
{
    public static class Extensions
    {
        private static WebStreamBuilder Get(IServiceProvider serviceProvider,Action<IServiceProvider,WebStreamBuilder> action)
        {
            var builder = new WebStreamBuilder();
            action(serviceProvider,builder);
            return builder;
        }
        public static IServiceCollection AddWebStream(this IServiceCollection services, Action<IServiceProvider,WebStreamBuilder> action, ServiceLifetime servicesLifetime = ServiceLifetime.Scoped)
        {
            if(servicesLifetime == ServiceLifetime.Scoped)
            {
                services.AddScoped<IPushEvent>(s =>Get(s,action).BuildEventSender());
                services.AddScoped<IReceiveEvent>(s => Get(s,action).BuildEventHandler());
            } 
            if (servicesLifetime == ServiceLifetime.Transient)
            {
                services.AddTransient<IPushEvent>(s => Get(s,action).BuildEventSender());
                services.AddTransient<IReceiveEvent>(s => Get(s, action).BuildEventHandler());
            } 
            if (servicesLifetime == ServiceLifetime.Singleton)
            {
                services.AddSingleton<IPushEvent>(s => Get(s,action).BuildEventSender());
                services.AddSingleton<IReceiveEvent>(s => Get(s, action).BuildEventHandler());
            }
            return services;
        }
    }
}
