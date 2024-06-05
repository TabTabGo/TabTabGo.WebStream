using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;

namespace TabTabGo.WebStream.Notification.EFCore.Repositories
{
    class EfNotificationRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<Entities.NotificationMessage, Guid>(context), INotificationRepository
    {
      
        public Task<PageList<Entities.NotificationMessage>> GetPageListAsync(List<Expression<Func<Entities.NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<Entities.NotificationMessage> query = context.Set<Entities.NotificationMessage>().AppleyCriteria(criteria);
            return new PageingListBuilder<Entities.NotificationMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        }

       
        public PageList<Entities.NotificationMessage> GetPageList(List<Expression<Func<Entities.NotificationMessage, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<Entities.NotificationMessage> query = context.Set<Entities.NotificationMessage>().AppleyCriteria(criteria);
            return new PageingListBuilder<Entities.NotificationMessage>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        }

        public List<Entities.NotificationMessage> GetByUserId(string userId)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Select(s => s.NotificationMessage).ToList();

        }

        public Task<List<Entities.NotificationMessage>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Select(s => s.NotificationMessage).ToListAsync(cancellationToken);
        }
    }
} 