using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Builders.ConnectionMangerBuilders;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.Notification.EFCore.Repositories;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;
using TabTabGo.WebStream.Services.Contract;
namespace TabTabGo.WebStream.Notification.EFCore
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
            builder.RegisteService<INotificationUserRepository, EfNotificationUserRepository>((s) => new EfNotificationUserRepository(s.GetRequiredService<DbContext>()));
            builder.RegisteService<INotificationRepository, EfNotificationRepository>((s) => new EfNotificationRepository(s.GetRequiredService<DbContext>()));
            builder.RegisteService<IUserConnectionRepository, EfUserConnectionRepository>((s) => new EfUserConnectionRepository(s.GetRequiredService<DbContext>()));
            builder.RegisteService<INotificationServices, DefaultNotificationServices>((s) => new DefaultNotificationServices());
            builder.RegisteService<IConnectionManager, StorageConnectionManager>((serviceProvider) => new StorageConnectionManager(serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IUserConnectionRepository>()));
            builder.RegisteService<IUserConnections, StorageUserConnections>((serviceProvider) => new StorageUserConnections(serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IUserConnectionRepository>()));
          

            return builder;
        }
    }
}
