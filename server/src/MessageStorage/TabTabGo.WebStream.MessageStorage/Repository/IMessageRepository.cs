using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.Repository
{
    public interface IMessageRepository  :TabTabGo.Core.Data.IGenericRepository<WebStreamStorageMessage, Guid>
    { 
        List<WebStreamStorageMessage> GetByUserId(string userId);   
        Task<List<WebStreamStorageMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);  
        Task<PageList<WebStreamStorageMessage>> GetPageListAsync(List<Expression<Func<WebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
        PageList<WebStreamStorageMessage> GetPageList(List<Expression<Func<WebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber); 
    }
}
