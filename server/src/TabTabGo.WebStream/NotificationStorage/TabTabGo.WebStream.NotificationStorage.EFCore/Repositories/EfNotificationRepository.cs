using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationStorage.Entites;
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
    }
}
