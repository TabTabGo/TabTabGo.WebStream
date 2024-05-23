using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    internal class EFUnitOfWorkFactory(DbContextOptions dbContextOptions) : INotificationUnitOfWorkFactory
    {
        public INotificationUnitOfWork Get()
        {
            return new EfNotificationUnitOfWork(dbContextOptions);
        }
    }
}
