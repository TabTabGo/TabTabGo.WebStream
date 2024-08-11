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
    public interface IMessageRepository  :TabTabGo.Core.Data.IGenericRepository<WebStreamStorageMessage, Guid>
    { 
        List<WebStreamStorageMessage> GetByUserId(UserIdData userId);   
        Task<List<WebStreamStorageMessage>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default);  
        Task<PageList<WebStreamStorageMessage>> GetPageListAsync(List<Expression<Func<WebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageList<WebStreamStorageMessage> GetPageList(List<Expression<Func<WebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
