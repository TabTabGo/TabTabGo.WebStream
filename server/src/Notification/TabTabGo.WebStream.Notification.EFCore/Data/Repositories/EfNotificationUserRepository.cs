using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;

namespace TabTabGo.WebStream.Notification.EFCore.Repositories
{
    class EfNotificationUserRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<NotificationUser, Guid>(context), INotificationUserRepository
    { 
        public Task<PageList<NotificationUser>> GetPageListAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<NotificationUser> query = context.Set<NotificationUser>().AppleyCriteria(criteria).Include(s=>s.NotificationMessage);
            return new PageingListBuilder<NotificationUser>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCountAsync(cancellationToken);
        }

        public PageList<NotificationUser> GetPageList(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<NotificationUser> query = context.Set<NotificationUser>().AppleyCriteria(criteria).Include(s => s.NotificationMessage);
            return new PageingListBuilder<NotificationUser>(query, pageNumber, pageSize, orderBy, isDesc).BuildWithFullCount();
        } 
        public List<NotificationUser> GetByUserId(string userId)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId)).Include(s=>s.NotificationMessage).ToList();
        } 
        public NotificationUser GetByUserIdAndNotificationId(string userId, Guid notificationId)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.NotificationMessageId.Equals(notificationId) && s.UserId.Equals(userId)).FirstOrDefault();
        } 
        public Task<NotificationUser> GetByUserIdAndNotificationIdAsync(string userId, Guid notificationId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.NotificationMessageId.Equals(notificationId) && s.UserId.Equals(userId)).FirstOrDefaultAsync(cancellationToken);
        } 
        public Task<List<NotificationUser>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.UserId.Equals(userId)).ToListAsync(cancellationToken);
        }

        public Task<NotificationUser> GetByUserIdAndUserNotificationId(string userId, Guid userNotificationId,CancellationToken cancellationToken)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.Id.Equals(userNotificationId) && s.UserId.Equals(userId)).FirstOrDefaultAsync(cancellationToken);

        }

        public NotificationUser GetByUserIdAndUserNotificationId(string userId, Guid userNotificationId)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.Id.Equals(userNotificationId) && s.UserId.Equals(userId)).FirstOrDefault();
        }
    }
}