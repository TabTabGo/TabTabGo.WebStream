using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.NotificationHub.Entities;
using TabTabGo.WebStream.NotificationHub.Module;
using TabTabGo.WebStream.NotificationHub.Repository;

namespace TabTabGo.WebStream.NotificationHub.EFCore.Repositories
{
    class EfNotificationRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<Entities.Notification, Guid>(context), INotificationRepository
    {
      
        public Task<PageList<Entities.Notification>> GetPageListAsync(List<Expression<Func<Entities.Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<Entities.Notification> query = context.Set<Entities.Notification>().AppleyCriteria(criteria);
            return new PageingListBuilder<Entities.Notification>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        }

       
        public PageList<Entities.Notification> GetPageList(List<Expression<Func<Entities.Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<Entities.Notification> query = context.Set<Entities.Notification>().AppleyCriteria(criteria);
            return new PageingListBuilder<Entities.Notification>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        }

        public List<Entities.Notification> GetByUserId(string userId)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Select(s => s.Notification).ToList();

        }

        public Task<List<Entities.Notification>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Select(s => s.Notification).ToListAsync(cancellationToken);
        }
    }
} 