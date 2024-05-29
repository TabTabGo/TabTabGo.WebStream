
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.MessageStorage.EFCore.Repositories;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
    public static class EfCoreMessagesStorageBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebStreamBuilder UseEfCoreMessageStorage(this WebStreamBuilder builder)
        {
            builder.RegisteService<IMessageRepository, EfMessageRepository>((s) =>
            {
                return new EfMessageRepository(s.GetRequiredService<DbContext>());
            });
            builder.RegisteService<IUserStreamStorageMessageRepository, EfUserWebStreamStorageMessageRepository>((s) =>
            {
                return new EfUserWebStreamStorageMessageRepository(s.GetRequiredService<DbContext>());
            }); 
            return builder;
        }
    }
}
