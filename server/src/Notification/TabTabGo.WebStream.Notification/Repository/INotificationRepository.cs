using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;

namespace TabTabGo.WebStream.NotificationHub.Repository
{
    public interface INotificationRepository  :TabTabGo.Core.Data.IGenericRepository<Entities.Notification,Guid>
    { 
        List<Entities.Notification> GetByUserId(string userId);   
        Task<List<Entities.Notification>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);  
        Task<PageList<Entities.Notification>> GetPageListAsync(List<Expression<Func<Entities.Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageList<Entities.Notification> GetPageList(List<Expression<Func<Entities.Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
