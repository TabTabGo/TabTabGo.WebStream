using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.NotificationHub.Builders;
using TabTabGo.WebStream.NotificationHub.EFCore.Repositories;
using TabTabGo.WebStream.NotificationHub.Repository;
using TabTabGo.WebStream.NotificationHub.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationHub.EFCore
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
