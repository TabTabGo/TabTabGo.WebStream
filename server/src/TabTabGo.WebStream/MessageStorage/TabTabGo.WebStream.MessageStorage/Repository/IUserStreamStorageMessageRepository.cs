using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.Repository
{
    public interface IUserStreamStorageMessageRepository: TabTabGo.Core.Data.IGenericRepository<UserWebStreamStorageMessage, Guid>
    { 
        Task<List<UserWebStreamStorageMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        List<UserWebStreamStorageMessage> GetByUserId(string userId);
        Task<UserWebStreamStorageMessage> GetByUserIdAndNotificationIdAsync(string userId, Guid notificationId, CancellationToken cancellationToken = default);
        UserWebStreamStorageMessage GetByUserIdAndNotificationId(string userId, Guid notificationId);  
        Task<PageList<UserWebStreamStorageMessage>> GetPageListAsync(List<Expression<Func<UserWebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize,int pageNumber, CancellationToken cancellationToken = default);
        PageList<UserWebStreamStorageMessage> GetPageList(List<Expression<Func<UserWebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
