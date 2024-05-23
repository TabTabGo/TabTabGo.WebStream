using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationStorage.EFCore.Repositories;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
    public static class EfCoreStorageBuilder
    {
        public static StorageBuilder UseEfCore(this StorageBuilder builder, Action<DbContextOptionsBuilder> action)
        {
            builder.SetUnitOfWork(() =>
            {
                DbContextOptionsBuilder contextOptions = new();
                action(contextOptions);
                return new EFUnitOfWorkFactory(contextOptions.Options);
            });
            return builder;
        }
    }
}
