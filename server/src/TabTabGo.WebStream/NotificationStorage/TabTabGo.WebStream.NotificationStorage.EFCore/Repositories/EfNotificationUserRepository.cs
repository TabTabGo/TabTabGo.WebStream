using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;
using TabTabGo.WebStream.NotificationStorage.Repository;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    class EfNotificationUserRepository(NotificationDbContext context) : INotificationUserRepository
    {
        public Guid Create(NotificationUser userNotification)
        {
            context.NotificationUsers.Entry(userNotification).State = EntityState.Added;
            context.SaveChanges();
            return userNotification.Id;
        }

        public async Task<Guid> CreateAsync(NotificationUser userNotification, CancellationToken cancellationToken = default)
        {
            context.NotificationUsers.Entry(userNotification).State = EntityState.Added;
            await context.SaveChangesAsync(cancellationToken);
            return userNotification.Id;
        } 
        public NotificationUser Find(Guid id)
        {
            return context.NotificationUsers.Find(id);
        }

        public Task<NotificationUser> FindAsync(Guid id, CancellationToken token = default)
        {
            return context.NotificationUsers.FindAsync(keyValues: [id], cancellationToken: token).AsTask();
        }

        public void Update(NotificationUser user)
        {
            context.NotificationUsers.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Task UpdateAsync(NotificationUser user, CancellationToken cancellationToken = default)
        {
            context.NotificationUsers.Entry(user).State = EntityState.Modified;
            return context.SaveChangesAsync(cancellationToken);
        } 

        public List<NotificationUser> FindByUserId(string userId)
        {
            return context.NotificationUsers.Where(s => s.UserId.Equals(userId)).ToList();
        }

        public NotificationUser FindByUserIdAndNotificationId(string userId, Guid notificationId)
        {
            return context.NotificationUsers.Where(s => s.NotificationId.Equals(notificationId) && s.UserId.Equals(userId)).FirstOrDefault();
        }

        public Task<NotificationUser> FindByUserIdAndNotificationIdAsync(string userId, Guid notificationId, CancellationToken cancellationToken = default)
        {
            return context.NotificationUsers.Where(s => s.NotificationId.Equals(notificationId) && s.UserId.Equals(userId)).FirstOrDefaultAsync(cancellationToken);

        }

        public Task<List<NotificationUser>> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.NotificationUsers.Where(s => s.UserId.Equals(userId)).ToListAsync(cancellationToken);
        }

        public Task<List<NotificationUser>> FindByCriteriaAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool descOrder, CancellationToken cancellationToken = default)
        {
            IQueryable<NotificationUser> query = context.NotificationUsers.AppleyCriteria(criteria);
            return query.OrderBy(orderBy, descOrder).ToListAsync(cancellationToken);
        }

        public List<NotificationUser> FindByCriteria(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool descOrder)
        {
            IQueryable<NotificationUser> query = context.NotificationUsers.AppleyCriteria(criteria);
            return query.OrderBy(orderBy, descOrder).ToList();
        }

        public Task<PageingResult<NotificationUser>> FindByCriteriaAsync(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            IQueryable<NotificationUser> query = context.NotificationUsers.AppleyCriteria(criteria);
            return new PageingResultBuilder<NotificationUser>(query, pageNumber, pageSize, orderBy, isDesc).BuildAsync(cancellationToken);
        }

        public PageingResult<NotificationUser> FindByCriteria(List<Expression<Func<NotificationUser, bool>>> criteria, string orderBy, bool isDesc, int pageSize, int pageNumber)
        {
            IQueryable<NotificationUser> query = context.NotificationUsers.AppleyCriteria(criteria);
            return new PageingResultBuilder<NotificationUser>(query, pageNumber, pageSize, orderBy, isDesc).Build();
        }
    }
}
