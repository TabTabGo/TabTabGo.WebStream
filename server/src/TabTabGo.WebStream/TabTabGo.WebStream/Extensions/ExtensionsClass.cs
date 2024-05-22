using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Extensions
{
    public static class ExtensionsClass
    {
        private static WebStreamBuilder Get(Action<WebStreamBuilder> action)
        {
            var builder = new WebStreamBuilder();
            action(builder);
            return builder;
        }
        public static IServiceCollection AddWebStream(this IServiceCollection services, Action<WebStreamBuilder> action, ServiceLifetime servicesLifetime = ServiceLifetime.Scoped)
        {
            if (servicesLifetime == ServiceLifetime.Scoped)
            {
                foreach (var item in Get(action).GetRegistedEventHandlers())
                {
                    services.AddScoped(item);
                }
                foreach (var item in Get(action).GetRegistedServices())
                {
                    services.AddScoped(item.GetTypeToRegest(), s=>item.Get());
                }
                services.AddScoped<IPushEvent>(s => Get(action).BuildIPushEvent(s));
                services.AddScoped<IReceiveEvent>(s => Get(action).BuildEventHandler(s));
            }
            if (servicesLifetime == ServiceLifetime.Transient)
            {
                foreach (var item in Get(action).GetRegistedEventHandlers())
                {
                    services.AddTransient(item);
                }
                foreach (var item in Get(action).GetRegistedServices())
                {
                    services.AddTransient(item.GetTypeToRegest(), s => item.Get());
                }
                services.AddTransient<IPushEvent>(s => Get(action).BuildIPushEvent(s));
                services.AddTransient<IReceiveEvent>(s => Get(action).BuildEventHandler(s));
            }
            if (servicesLifetime == ServiceLifetime.Singleton)
            {
                foreach (var item in Get(action).GetRegistedEventHandlers())
                {
                    services.AddSingleton(item);
                }
                foreach (var item in Get(action).GetRegistedServices())
                {
                    services.AddSingleton(item.GetTypeToRegest(), s => item.Get());
                }
                services.AddSingleton<IPushEvent>(s => Get(action).BuildIPushEvent(s));
                services.AddSingleton<IReceiveEvent>(s => Get(action).BuildEventHandler(s));
            }
            return services;
        }
    }
}
