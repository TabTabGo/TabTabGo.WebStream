using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.MessageStorage.Repository
{
    public interface IUserStreamStorageMessageRepository: TabTabGo.Core.Data.IGenericRepository<UserWebStreamStorageMessage, Guid>
    { 
        Task<List<UserWebStreamStorageMessage>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default);
        List<UserWebStreamStorageMessage> GetByUserId(UserIdData userId);
        Task<UserWebStreamStorageMessage> GetByUserIdAndNotificationIdAsync(UserIdData userId, Guid notificationId, CancellationToken cancellationToken = default);
        UserWebStreamStorageMessage GetByUserIdAndNotificationId(UserIdData userId, Guid notificationId);  
        Task<PageList<UserWebStreamStorageMessage>> GetPageListAsync(List<Expression<Func<UserWebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize,int pageNumber, CancellationToken cancellationToken = default);
        PageList<UserWebStreamStorageMessage> GetPageList(List<Expression<Func<UserWebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
