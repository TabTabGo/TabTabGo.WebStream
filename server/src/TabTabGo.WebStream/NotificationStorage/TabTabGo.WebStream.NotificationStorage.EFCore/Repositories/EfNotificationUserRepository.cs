using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationStorage.Entites;
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
    }
}
