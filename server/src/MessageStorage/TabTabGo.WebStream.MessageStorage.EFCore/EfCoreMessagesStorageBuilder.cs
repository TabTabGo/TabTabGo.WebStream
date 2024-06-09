
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.MessageStorage.EFCore.Repositories;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.MessageStorage.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Notification.EFCore
{
    public static class EfCoreMessagesStorageBuilder
    {
        /// <summary>
        /// by using this EF Core Storage will be used to Store UserConnections also it will add needed implementation for store messages if you wnat to use PushToStorage Implementation
        /// <br/>
        /// you have to setup IUnitOfWork from TabTabGo.Core alone please add it to you service Collectoin
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebStreamBuilder UseEFCore(this WebStreamBuilder builder)
        {
            builder.RegisteService<IMessageRepository, EfMessageRepository>((s) =>
            {
                return new EfMessageRepository(s.GetRequiredService<DbContext>());
            });
            builder.RegisteService<IUserStreamStorageMessageRepository, EfUserWebStreamStorageMessageRepository>((s) =>
            {
                return new EfUserWebStreamStorageMessageRepository(s.GetRequiredService<DbContext>());
            });
            builder.RegisteService<IUserConnectionRepository, EfUserConnectionRepository>((s) => new EfUserConnectionRepository(s.GetRequiredService<DbContext>())); 
            builder.RegisteService<IUserConnections, StorageUserConnections>((serviceProvider) => new StorageUserConnections(serviceProvider.GetRequiredService<IUnitOfWork>(), serviceProvider.GetRequiredService<IUserConnectionRepository>()));
            return builder;
        }
    }
}
