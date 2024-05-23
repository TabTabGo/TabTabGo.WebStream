using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    class EfNotificationRepository(NotificationDbContext context) : INotificationRepository
    {
        public Guid Create(Notification notification)
        {
            context.Notifications.Entry(notification).State = EntityState.Added;
            context.SaveChanges();
            return notification.Id;
        }

        public async Task<Guid> CreateAsync(Notification notification, CancellationToken cancellationToken = default)
        {
            context.Notifications.Entry(notification).State = EntityState.Added;
            await context.SaveChangesAsync(cancellationToken);
            return notification.Id;
        }

        public Notification Find(Guid id)
        {
            return context.Notifications.Find(id);
        }

        public Task<Notification> FindAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return context.Notifications.FindAsync(keyValues: [id], cancellationToken: cancellationToken).AsTask();
        }

        public List<Notification> FindByUserId(string userId)
        {
            return context.NotificationUsers.Where(s => s.UserId.Equals(userId)).Select(s => s.Notification).ToList();

        }

        public Task<List<Notification>> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.NotificationUsers.Where(s => s.UserId.Equals(userId)).Select(s => s.Notification).ToListAsync(cancellationToken);
        }


        public Task<List<Notification>> FindByCriteria(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, CancellationToken cancellationToken = default)
        {
            IQueryable<Notification> query = context.Notifications.AppleyCriteria(criteria);
            return query.OrderBy(orderBy, isDesc).ToListAsync(cancellationToken);
        }

        public Task<PageingResult<Notification>> FindByCriteria(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<Notification> query = context.Notifications.AppleyCriteria(criteria);
            return new PageingResultBuilder<Notification>(query, pageNumber, pageSize, orderBy, isDesc).BuildAsync(cancellationToken);
        }

        public List<Notification> FindByCriteriaAsync(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc)
        {
            IQueryable<Notification> query = context.Notifications.AppleyCriteria(criteria);
            return query.OrderBy(orderBy, isDesc).ToList();
        }

        public PageingResult<Notification> FindByCriteriaAsync(List<Expression<Func<Notification, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<Notification> query = context.Notifications.AppleyCriteria(criteria);
            return new PageingResultBuilder<Notification>(query, pageNumber, pageSize, orderBy, isDesc).Build();
        }

    }
}
