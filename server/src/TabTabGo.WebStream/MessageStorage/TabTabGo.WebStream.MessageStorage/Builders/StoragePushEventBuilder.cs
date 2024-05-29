
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.MessageStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.MessageStorage.Builders
{
    public static class StoragePushEventBuilder
    {
        /// <summary>
        /// by using this decoration, all  messages you send to stream will be stored in message dataBase. <br/> 
        /// </summary>
        /// <param name="webStreamBuilder"></param>
        /// <returns></returns>
        public static PushEventBuilder AddPushToStorage(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushToStorageService(serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IMessageRepository>(), serviceProvider.GetRequiredService<IUserStreamStorageMessageRepository>()));
            return webStreamBuilder;
        }
        /// <summary>
        /// by using this decoration, all  messages you send to stream will be stored in dataBase. <br/>
        /// the deffrent between this and AddPushToStorage is that this will store message after send successs. <br/> 
        /// </summary>
        public static PushEventBuilder PushToStorageOnSuccess(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.DecorateWith((serviceProvider, oldService) => new PushToStorageSucessOnDecorator(oldService, serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IMessageRepository>(), serviceProvider.GetRequiredService<IUserStreamStorageMessageRepository>()));
            return webStreamBuilder;
        }
    }
}
