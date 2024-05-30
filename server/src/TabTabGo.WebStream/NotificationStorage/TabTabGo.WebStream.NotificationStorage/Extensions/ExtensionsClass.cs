using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Extensions;

namespace TabTabGo.WebStream.NotificationStorage.Extensions
{
    public static class ExtensionsClass
    {
        private static NotificationBuilder Get(Action<NotificationBuilder> action)
        {
            var builder = new NotificationBuilder();
            action(builder);
            return builder;
        }
        public static IServiceCollection AddNotificationServices(this IServiceCollection services, Action<NotificationBuilder> action, ServiceLifetime servicesLifetime = ServiceLifetime.Scoped)
        {
            var builder = Get(action);
            services.AddWebStream(builder.GetWebStreamBuilderAction(), servicesLifetime);
            if (servicesLifetime == ServiceLifetime.Scoped)
            {
                foreach (var item in builder.GetRegistedServices())
                {
                    services.AddScoped(item.GetTypeToRegest(), s => item.Get(s));
                }
            }
            if (servicesLifetime == ServiceLifetime.Transient)
            {
                foreach (var item in builder.GetRegistedServices())
                {
                    services.AddTransient(item.GetTypeToRegest(), s => item.Get(s));
                }
            }
            if (servicesLifetime == ServiceLifetime.Singleton)
            {
                foreach (var item in builder.GetRegistedServices())
                {
                    services.AddSingleton(item.GetTypeToRegest(), s => item.Get(s));
                }
            }
            return services;
        }
    }
}
