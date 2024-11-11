
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.Model;

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
        public List<UserWebStreamStorageMessage> GetByUserId(UserIdData userId)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToList();
        } 
        public UserWebStreamStorageMessage GetByUserIdAndNotificationId(UserIdData userId, Guid messageId)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.MessageId.Equals(messageId) && s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).FirstOrDefault();
        } 
        public Task<UserWebStreamStorageMessage> GetByUserIdAndNotificationIdAsync(UserIdData userId, Guid messageId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.MessageId.Equals(messageId) && s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).FirstOrDefaultAsync(cancellationToken);
        } 
        public Task<List<UserWebStreamStorageMessage>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToListAsync(cancellationToken);
        }
    }
}