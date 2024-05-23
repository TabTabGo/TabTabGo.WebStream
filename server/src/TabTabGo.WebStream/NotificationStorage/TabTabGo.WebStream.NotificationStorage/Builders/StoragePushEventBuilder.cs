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
        public static PushEventBuilder AddPushToStorage(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushToStorageService(serviceProvider.GetRequiredService<IUserConnections>(), serviceProvider.GetRequiredService<INotificationUnitOfWork>()));
            return webStreamBuilder;
        }
        public static WebStreamBuilder SetUpStorage(this WebStreamBuilder webStreamBuilder, Action<StorageBuilder> action)
        {
            webStreamBuilder.RegisteService<INotificationUnitOfWork, INotificationUnitOfWork>(() =>
            {
                StorageBuilder bulder = new();
                action(bulder);
                return bulder.GetUnitOfWork();
            });
            return webStreamBuilder;
        }
    }
}
