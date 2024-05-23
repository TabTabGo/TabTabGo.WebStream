namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationUnitOfWorkFactory
    {
        INotificationUnitOfWork Get();
    }
}
