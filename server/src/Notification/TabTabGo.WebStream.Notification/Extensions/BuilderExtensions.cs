using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Extensions;
using TabTabGo.WebStream.Notification.Builders;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;
using TabTabGo.WebStream.NotificationStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Notification.Extensions
{
    public static class BuilderExtensions
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
                services.AddScoped<INotificationServices<string>, DefaultNotificationServices>();
                services.AddScoped<ISendNotification, SendNotificationService>();

            }
            if (servicesLifetime == ServiceLifetime.Transient)
            {
                foreach (var item in builder.GetRegistedServices())
                {
                    services.AddTransient(item.GetTypeToRegest(), s => item.Get(s));
                }
                services.AddTransient<INotificationServices<string>, DefaultNotificationServices>();
                services.AddTransient<ISendNotification, SendNotificationService>();

            }
            if (servicesLifetime == ServiceLifetime.Singleton)
            {
                foreach (var item in builder.GetRegistedServices())
                {
                    services.AddSingleton(item.GetTypeToRegest(), s => item.Get(s));
                }
                services.AddSingleton<INotificationServices<string>, DefaultNotificationServices>();
                services.AddSingleton<ISendNotification, SendNotificationService>();
            }
            return services;
        }
    }
}
