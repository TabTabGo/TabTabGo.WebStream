using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Entities.Enums;

namespace TabTabGo.WebStream.Notification.Repository
{
    public interface INotificationUserRepository: TabTabGo.Core.Data.IGenericRepository<NotificationUser, Guid>
    { 
        Task<List<NotificationUser>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default);
        List<NotificationUser> GetByUserId(UserIdData userId);
        Task<NotificationUser> GetByUserIdAndNotificationIdAsync(UserIdData userId, Guid notificationId,
            CancellationToken cancellationToken = default);
        NotificationUser GetByUserIdAndNotificationId(UserIdData userId, Guid notificationId);

        NotificationUser GetByUserIdAndUserNotificationId(UserIdData userId, Guid userNotificationId);
        Task<NotificationUser> GetByUserIdAndUserNotificationId(UserIdData userId, Guid userNotificationId, CancellationToken cancellationToken);
        
        Task<PageList<NotificationUser>> GetPageListAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize,int pageNumber, CancellationToken cancellationToken = default);
        PageList<NotificationUser> GetPageList(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber);
        void UpdateAllUnreadNotifications(UserIdData userId, NotificationUserStatus read, DateTime utcNow);
        Task UpdateAllUnreadNotificationsAsync(UserIdData userId, NotificationUserStatus read, DateTime utcNow);
    }
}
