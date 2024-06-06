using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;

namespace TabTabGo.WebStream.Notification.Repository
{
    public interface INotificationRepository  :TabTabGo.Core.Data.IGenericRepository<Entities.NotificationMessage,Guid>
    { 
        List<Entities.NotificationMessage> GetByUserId(string userId);   
        Task<List<Entities.NotificationMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);  
        Task<PageList<Entities.NotificationMessage>> GetPageListAsync(List<Expression<Func<Entities.NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageList<Entities.NotificationMessage> GetPageList(List<Expression<Func<Entities.NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
