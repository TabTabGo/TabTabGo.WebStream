using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.Core.Data;
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
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushToStorageService(serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>() ));
            return webStreamBuilder;
        }
        /// <summary>
        /// use only one of PushToStorageOnSuccess or  AddPushToStorage 
        /// </summary>
        /// <param name="webStreamBuilder"></param>
        /// <returns></returns>
        public static PushEventBuilder PushToStorageOnSuccess(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.DecorateWith((serviceProvider, oldService) => new PushToStorageSucessOnDecorator(oldService, serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>()));
            return webStreamBuilder;
        } 
    }
}
