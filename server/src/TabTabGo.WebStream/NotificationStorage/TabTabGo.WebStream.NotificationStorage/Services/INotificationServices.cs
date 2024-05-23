using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    public interface INotificationServices
    {
        void ReadNotification(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository);
        Task ReadNotificationAsync(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default);
        PageingResult<NotificationUser> GetUserNotifications(string userId, UserNotificationFilter filters, PagingParameters pagingParameters, INotificationUserRepository notificationUserRepository);
        Task<PageingResult<NotificationUser>> GetUserNotificationsAsync(string UserId, UserNotificationFilter filters, PagingParameters pagingParameters, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default);
    }
}
