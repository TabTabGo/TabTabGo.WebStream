using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;

namespace TabTabGo.WebStream.Notification.Services
{
    public interface INotificationServices<in TUserKey>
    {
        Task ReadAllNotifications(TUserKey userKey, CancellationToken cancellationToken = default);
        Task ReadNotification(NotificationUser notificationUser, CancellationToken cancellationToken = default);
        Task<TabTabGo.Core.Models.PageList<NotificationUser>> GetUserNotifications(string userId, UserNotificationFilter filters, TabTabGo.Core.ViewModels.PagingOptionRequest pagingParameters, CancellationToken cancellationToken = default);
    }
}