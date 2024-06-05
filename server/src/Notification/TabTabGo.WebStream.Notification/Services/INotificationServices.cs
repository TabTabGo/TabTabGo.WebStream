using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationHub.Entities;
using TabTabGo.WebStream.NotificationHub.Module;
using TabTabGo.WebStream.NotificationHub.Repository;

namespace TabTabGo.WebStream.NotificationHub.Services
{
    public interface INotificationServices
    {
        void ReadNotification(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository);
        Task ReadNotificationAsync(NotificationUser notificationUser, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default);
        TabTabGo.Core.Models.PageList<NotificationUser> GetUserNotifications(string userId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, INotificationUserRepository notificationUserRepository);
        Task<TabTabGo.Core.Models.PageList<NotificationUser>> GetUserNotificationsAsync(string userId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, INotificationUserRepository notificationUserRepository, CancellationToken cancellationToken = default);
    }
}
