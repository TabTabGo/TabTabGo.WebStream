using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationRepository  :TabTabGo.Core.Data.IGenericRepository<Notification,Guid>
    { 
        List<Notification> FindByUserId(string UserId);   
        Task<List<Notification>> FindByUserIdAsync(string UserId, CancellationToken cancellationToken = default); 
        Task<List<Notification>> FindByCriteria(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, CancellationToken cancellationToken = default);
        List<Notification> FindByCriteriaAsync(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc);
        Task<PageingResult<Notification>> FindByCriteria(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageingResult<Notification> FindByCriteriaAsync(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
