using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.NotificationStorage.EFCore.Repositories;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.NotificationStorage.Services;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
    public static class EfCoreStorageBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebStreamBuilder UseEfCore(this WebStreamBuilder builder)
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


            return builder;
        }
    }
}
