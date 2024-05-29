using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.NotificationStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Builders
{
    public static class StoragePushEventBuilder
    {
        /// <summary>
        /// by using this decoration, all  messages you send to stream will be stored in dataBase. <br/>
        /// it is the implementation of IPushEvent using the logic of ISendNotification.<br/>
        /// <b> so if you added it just use IPushEvent and you dont need to use ISendNotification.<br/> 
        /// </summary>
        /// <param name="webStreamBuilder"></param>
        /// <returns></returns>
        public static PushEventBuilder AddPushToStorage(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushToStorageService(serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>()));
            return webStreamBuilder;
        }
        /// <summary>
        /// by using this decoration, all  messages you send to stream will be stored in dataBase. <br/>
        /// the deffrent between this and AddPushToStorage is that this will store message after send successs. <br/>
        /// it is the implementation of IPushEvent using the logic of ISendNotification.<br/>
        /// <b> so if you added it just use IPushEvent and you dont need to use ISendNotification.<br/> 
        /// </summary>
        public static PushEventBuilder PushToStorageOnSuccess(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.DecorateWith((serviceProvider, oldService) => new PushToStorageSucessOnDecorator(oldService, serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<INotificationRepository>(), serviceProvider.GetRequiredService<INotificationUserRepository>()));
            return webStreamBuilder;
        }
    }
}
