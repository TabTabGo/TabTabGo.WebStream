using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.NotificationStorage.EFCore.Repositories;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.NotificationStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
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
            builder.RegisteService<INotificationUserRepository, EfNotificationUserRepository>((s) =>
            {
                return new EfNotificationUserRepository(s.GetRequiredService<DbContext>());
            });
            builder.RegisteService<INotificationRepository, EfNotificationRepository>((s) =>
            {
                return new EfNotificationRepository(s.GetRequiredService<DbContext>());
            });

            builder.RegisteService<INotificationServices, DefaultNotificationServices>((s) =>
            {
                return new DefaultNotificationServices();
            }); 
            builder.RegisteService<ISendNotification, SendNotificationService>((serviceProvider) =>
            {
                return new SendNotificationService(serviceProvider.GetRequiredService<IPushEvent>() , serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>() );
            });
            return builder;
        }
    }
}
