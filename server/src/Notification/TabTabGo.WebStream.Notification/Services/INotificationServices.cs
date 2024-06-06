using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;

namespace TabTabGo.WebStream.Notification.Services
{
    public interface INotificationServices
    {
        void ReadNotification(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository);
        Task ReadNotificationAsync(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default);
        TabTabGo.Core.Models.PageList<NotificationUser> GetUserNotifications(string userId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, INotificationUserRepository notificationUserRepository);
        Task<TabTabGo.Core.Models.PageList<NotificationUser>> GetUserNotificationsAsync(string UserId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default);
    }
}
