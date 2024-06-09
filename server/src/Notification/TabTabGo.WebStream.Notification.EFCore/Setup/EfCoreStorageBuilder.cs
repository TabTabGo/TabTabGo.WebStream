using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Notification.Builders;
using TabTabGo.WebStream.Notification.EFCore.Repositories;
using TabTabGo.WebStream.Notification.Repository; 
namespace TabTabGo.WebStream.Notification.EFCore
{
    public static class EfCoreStorageBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static NotificationBuilder UseEfCore(this NotificationBuilder builder)
        {
            builder.RegisteService<INotificationUserRepository, EfNotificationUserRepository>((s) => new EfNotificationUserRepository(s.GetRequiredService<DbContext>()));
            builder.RegisteService<INotificationRepository, EfNotificationRepository>((s) => new EfNotificationRepository(s.GetRequiredService<DbContext>()));
           return builder;
        }
    }
}
