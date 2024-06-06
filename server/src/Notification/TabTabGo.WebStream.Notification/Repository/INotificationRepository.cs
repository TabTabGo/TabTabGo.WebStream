using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;

namespace TabTabGo.WebStream.Notification.Repository
{
    public interface INotificationRepository  :TabTabGo.Core.Data.IGenericRepository<NotificationMessage,Guid>
    { 
        List<NotificationMessage> GetByUserId(string userId);   
        Task<List<NotificationMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);  
        Task<PageList<NotificationMessage>> GetPageListAsync(List<Expression<Func<NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageList<NotificationMessage> GetPageList(List<Expression<Func<NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
