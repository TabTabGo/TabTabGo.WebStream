using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Notification.Builders;
using TabTabGo.WebStream.Notification.EFCore.Repositories;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Notification.EFCore
{
    public static class EfCoreNotificationStorageBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NotificationBuilder UseEfCoreNotificationStorage(this NotificationBuilder builder)
        {
            builder.RegisteService<INotificationUserRepository, EfNotificationUserRepository>((s) => new EfNotificationUserRepository(s.GetRequiredService<DbContext>()));
            builder.RegisteService<INotificationRepository, EfNotificationRepository>((s) => new EfNotificationRepository(s.GetRequiredService<DbContext>()));

            builder.RegisteService<INotificationServices, DefaultNotificationServices>((s) => new DefaultNotificationServices()); 
            builder.RegisteService<ISendNotification, SendNotificationService>((serviceProvider) => new SendNotificationService(serviceProvider.GetRequiredService<IPushEvent>() , serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>() ));
            return builder;
        }
    }
}
