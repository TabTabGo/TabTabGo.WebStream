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
            builder.RegisteService<IUserConnectionRepository, EfUserConnectionRepository>((s) =>
            {
                return new EfUserConnectionRepository(s.GetRequiredService<DbContext>());
            });
            builder.RegisteService<INotificationServices, DefaultNotificationServices>((s) =>
            {
                return new DefaultNotificationServices();
            });
            builder.RegisteService<ISaveWebStreamMessage, PushToStorageService>((serviceProvider) =>
            {
                return new PushToStorageService(serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>());
            });
            builder.RegisteService<ISendNotification, PushToStorageSucessOnDecorator>((serviceProvider) =>
            {
                return new PushToStorageSucessOnDecorator(serviceProvider.GetRequiredService<IPushEvent>() , serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>() );
            });
            builder.RegisteService<IConnectionManager, StorageConnectionManager>((serviceProvider) =>
            {
                return new StorageConnectionManager(serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IUserConnectionRepository>());
            });
            builder.RegisteService<IUserConnections, StorageUserConnections>((serviceProvider) =>
            {
                return new StorageUserConnections(serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IUserConnectionRepository>());
            });
          

            return builder;
        }
    }
}
