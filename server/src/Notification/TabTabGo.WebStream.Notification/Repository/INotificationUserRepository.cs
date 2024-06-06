using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.Repository
{
    public interface INotificationUserRepository: TabTabGo.Core.Data.IGenericRepository<NotificationUser, Guid>
    { 
        Task<List<NotificationUser>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        List<NotificationUser> GetByUserId(string userId);
        Task<NotificationUser> GetByUserIdAndNotificationIdAsync(string userId, Guid notificationId,
            CancellationToken cancellationToken = default);
        NotificationUser GetByUserIdAndNotificationId(string userId, Guid notificationId);  
        Task<PageList<NotificationUser>> GetPageListAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize,int pageNumber, CancellationToken cancellationToken = default);
        PageList<NotificationUser> GetPageList(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
