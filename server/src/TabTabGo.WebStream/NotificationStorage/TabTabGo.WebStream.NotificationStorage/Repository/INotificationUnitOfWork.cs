using System;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationUnitOfWork : IDisposable
    {
        INotificationRepository Notifications { get; }
        INotificationUserRepository UserRepository { get; }
        INotificationTransaction StartTransaction();

    }
}
