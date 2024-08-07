using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.Model;
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
        public List<NotificationUser> GetByUserId(UserIdData userId)
        {
            return context.Set<NotificationUser>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).Include(s=>s.NotificationMessage).ToList();
        } 
        public NotificationUser GetByUserIdAndNotificationId(UserIdData userId, Guid notificationId)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.NotificationMessageId.Equals(notificationId) && s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).FirstOrDefault();
        } 
        public Task<NotificationUser> GetByUserIdAndNotificationIdAsync(UserIdData userId, Guid notificationId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.NotificationMessageId.Equals(notificationId) && s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).FirstOrDefaultAsync(cancellationToken);
        } 
        public Task<List<NotificationUser>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToListAsync(cancellationToken);
        } 
        public Task<NotificationUser> GetByUserIdAndUserNotificationId(UserIdData userId, Guid userNotificationId,CancellationToken cancellationToken)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.Id.Equals(userNotificationId) && s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).FirstOrDefaultAsync(cancellationToken);
        } 
        public NotificationUser GetByUserIdAndUserNotificationId(UserIdData userId, Guid userNotificationId)
        {
            return context.Set<NotificationUser>().Include(s => s.NotificationMessage).Where(s => s.Id.Equals(userNotificationId) && s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).FirstOrDefault();
        }
    }
}