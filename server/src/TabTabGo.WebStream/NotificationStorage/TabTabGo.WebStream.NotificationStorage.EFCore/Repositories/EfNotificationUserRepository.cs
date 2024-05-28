using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.Core.Models;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    class EfNotificationUserRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<NotificationUser, Guid>(context), INotificationUserRepository
    {
        public Task<List<NotificationUser>> FindByCriteriaAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool descOrder, CancellationToken cancellationToken = default)
        {
            IQueryable<NotificationUser> query = context.Set<NotificationUser>().AppleyCriteria(criteria);
            return query.OrderBy(orderBy, descOrder).ToListAsync(cancellationToken);
        }

        public List<NotificationUser> FindByCriteria(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool descOrder)
        {
            IQueryable<NotificationUser> query = context.Set<NotificationUser>().AppleyCriteria(criteria);
            return query.OrderBy(orderBy, descOrder).ToList();
        }

        public Task<PageList<NotificationUser>> FindByCriteriaAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<NotificationUser> query = context.Set<NotificationUser>().AppleyCriteria(criteria);
            return new PageingListBuilder<NotificationUser>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        }

        public PageList<NotificationUser> FindByCriteria(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<NotificationUser> query = context.Set<NotificationUser>().AppleyCriteria(criteria);
            return new PageingListBuilder<NotificationUser>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        } 
        public List<NotificationUser> FindByUserId(string userId)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).ToList();
        } 
        public NotificationUser FindByUserIdAndNotificationId(string userId, Guid notificationId)
        {
            return context.Set<NotificationUser>().Where(s => s.NotificationId.Equals(notificationId) && s.UserId.Equals(userId)).FirstOrDefault();
        } 
        public Task<NotificationUser> FindByUserIdAndNotificationIdAsync(string userId, Guid notificationId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Where(s => s.NotificationId.Equals(notificationId) && s.UserId.Equals(userId)).FirstOrDefaultAsync(cancellationToken);
        } 
        public Task<List<NotificationUser>> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).ToListAsync(cancellationToken);
        }
    }
}