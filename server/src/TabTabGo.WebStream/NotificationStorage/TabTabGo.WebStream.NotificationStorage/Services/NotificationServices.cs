using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    public interface NotificationServices
    {
        void ReadNotification(Guid notificationId, string UserId);
        Task ReadNotificationAsync(Guid notificationId, string UserId, CancellationToken cancellationToken = default); 
        NotificationUser GetUserNotification(Guid notificationId, string userId);
        Task<NotificationUser> GetUserNotificationAsync(Guid notificationId, string UserId, CancellationToken cancellationToken = default); 
        List<NotificationUser> GetUserNotifications(string user,/*create filter Class*/ object filters);
        Task<List<NotificationUser>> GetUserNotificationsAsync(string UserId,/* create filter Class */ object filters, CancellationToken cancellationToken = default);
    }
}
