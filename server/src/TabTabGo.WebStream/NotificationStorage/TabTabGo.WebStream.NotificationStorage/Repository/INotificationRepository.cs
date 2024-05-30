using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationRepository  :TabTabGo.Core.Data.IGenericRepository<Notification,Guid>
    { 
        List<Notification> GetByUserId(string UserId);   
        Task<List<Notification>> GetByUserIdAsync(string UserId, CancellationToken cancellationToken = default);  
        Task<PageList<Notification>> GetPageListAsync(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageList<Notification> GetPageList(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
