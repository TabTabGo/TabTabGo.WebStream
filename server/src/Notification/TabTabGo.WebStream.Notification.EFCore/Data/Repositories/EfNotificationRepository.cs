using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;

namespace TabTabGo.WebStream.Notification.EFCore.Repositories
{
    class EfNotificationRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<NotificationMessage, Guid>(context), INotificationRepository
    {
      
        public Task<PageList<NotificationMessage>> GetPageListAsync(List<Expression<Func<NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<NotificationMessage> query = context.Set<NotificationMessage>().AppleyCriteria(criteria);
            return new PagingListBuilder<NotificationMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        }

       
        public PageList<NotificationMessage> GetPageList(List<Expression<Func<NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<NotificationMessage> query = context.Set<NotificationMessage>().AppleyCriteria(criteria);
            return new PagingListBuilder<NotificationMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        }

        public List<NotificationMessage> GetByUserId(string userId)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Select(s => s.NotificationMessage).ToList();

        }

        public Task<List<NotificationMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Select(s => s.NotificationMessage).ToListAsync(cancellationToken);
        }
    }
} 