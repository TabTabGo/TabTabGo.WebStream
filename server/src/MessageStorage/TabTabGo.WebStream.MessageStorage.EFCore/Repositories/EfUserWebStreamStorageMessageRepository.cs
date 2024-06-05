
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Repository; 

namespace TabTabGo.WebStream.MessageStorage.EFCore.Repositories
{
    class EfUserWebStreamStorageMessageRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<UserWebStreamStorageMessage, Guid>(context), IUserStreamStorageMessageRepository
    { 
        public Task<PageList<UserWebStreamStorageMessage>> GetPageListAsync(List<Expression<Func<UserWebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<UserWebStreamStorageMessage> query = context.Set<UserWebStreamStorageMessage>().ApplyCriteria(criteria);
            return new PageingListBuilder<UserWebStreamStorageMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        }

        public PageList<UserWebStreamStorageMessage> GetPageList(List<Expression<Func<UserWebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<UserWebStreamStorageMessage> query = context.Set<UserWebStreamStorageMessage>().ApplyCriteria(criteria);
            return new PageingListBuilder<UserWebStreamStorageMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        } 
        public List<UserWebStreamStorageMessage> GetByUserId(string userId)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.UserId.Equals(userId)).ToList();
        } 
        public UserWebStreamStorageMessage GetByUserIdAndNotificationId(string userId, Guid messageId)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.MessageId.Equals(messageId) && s.UserId.Equals(userId)).FirstOrDefault();
        } 
        public Task<UserWebStreamStorageMessage> GetByUserIdAndNotificationIdAsync(string userId, Guid messageId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.MessageId.Equals(messageId) && s.UserId.Equals(userId)).FirstOrDefaultAsync(cancellationToken);
        } 
        public Task<List<UserWebStreamStorageMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.UserId.Equals(userId)).ToListAsync(cancellationToken);
        }
    }
}