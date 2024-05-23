using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    class EfNotificationUnitOfWork : INotificationUnitOfWork
    {
        readonly NotificationDbContext _notificationDbContext;
        public EfNotificationUnitOfWork(DbContextOptions options)
        {
            _notificationDbContext = new NotificationDbContext(options);
            _notificationDbContext.Database.EnsureCreated();
        }
        private INotificationRepository _notifications = null;
        private INotificationUserRepository _userRepository = null;
        public INotificationRepository Notifications => _notifications ??= new EfNotificationRepository(_notificationDbContext);

        public INotificationUserRepository UserRepository => _userRepository  ??= new EfNotificationUserRepository(_notificationDbContext);

        public void Dispose()
        {
            _notificationDbContext.Dispose();
        }

        public INotificationTransaction StartTransaction()
        {
            return new EFNotificationStorageTransaction(_notificationDbContext.Database.BeginTransaction());
        }
    }
}
