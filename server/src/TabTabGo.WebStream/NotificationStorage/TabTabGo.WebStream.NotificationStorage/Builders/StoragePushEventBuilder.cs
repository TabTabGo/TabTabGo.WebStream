using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.NotificationStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Builders
{
    public static class StoragePushEventBuilder
    {
        /// <summary>
        /// use only one of PushToStorageOnSuccess or  AddPushToStorage 
        /// </summary>
        /// <param name="webStreamBuilder"></param>
        /// <returns></returns>
        public static PushEventBuilder AddPushToStorage(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushToStorageService(serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<INotificationUnitOfWorkFactory>()));
            return webStreamBuilder;
        }
        /// <summary>
        /// use only one of PushToStorageOnSuccess or  AddPushToStorage 
        /// </summary>
        /// <param name="webStreamBuilder"></param>
        /// <returns></returns>
        public static PushEventBuilder PushToStorageOnSuccess(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.DecorateWith((serviceProvider, oldService) => new PushToStorageSucessOnDecorator(oldService, serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<INotificationUnitOfWorkFactory>()));
            return webStreamBuilder;
        }
        public static WebStreamBuilder SetUpStorage(this WebStreamBuilder webStreamBuilder, Action<StorageBuilder> action)
        {
            webStreamBuilder.RegisteService<INotificationUnitOfWorkFactory, INotificationUnitOfWorkFactory>(() =>
            {
                StorageBuilder bulder = new StorageBuilder();
                action(bulder);
                return bulder.GetUnitOfWork();
            });
            webStreamBuilder.RegisteService<INotificationServices, DefaultNotificationServices>(() =>
            {
                return new DefaultNotificationServices();
            });
            return webStreamBuilder;
        }
    }
}
