using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.MessageStorage;
namespace TabTabGo.WebStream.MessageStorage.EFCore.Repositories
{
    class EfMessageRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<WebStreamStorageMessage, Guid>(context), IMessageRepository
    { 
        public Task<PageList<WebStreamStorageMessage>> GetPageListAsync(List<Expression<Func<WebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<WebStreamStorageMessage> query = context.Set<WebStreamStorageMessage>().AppleyCriteria(criteria);
            return new PageingListBuilder<WebStreamStorageMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        } 
        public PageList<WebStreamStorageMessage> GetPageList(List<Expression<Func<WebStreamStorageMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<WebStreamStorageMessage> query = context.Set<WebStreamStorageMessage>().AppleyCriteria(criteria);
            return new PageingListBuilder<WebStreamStorageMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        } 
        public List<WebStreamStorageMessage> GetByUserId(string userId)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.UserId.Equals(userId)).Select(s => s.Message).ToList(); 
        } 
        public Task<List<WebStreamStorageMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserWebStreamStorageMessage>().Where(s => s.UserId.Equals(userId)).Select(s => s.Message).ToListAsync(cancellationToken);
        }
    }
} 