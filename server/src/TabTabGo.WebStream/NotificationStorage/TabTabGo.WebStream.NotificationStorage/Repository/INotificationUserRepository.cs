using System;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationUserRepository
    {
        Guid Create(NotificationUser notificationUser);
        void Update(NotificationUser notificationUser);
        NotificationUser Find(Guid notificationUserId);  
        Task<Guid> CreateAsync(NotificationUser notificationUser, CancellationToken cancellationToken = default);
        Task UpdateAsync(NotificationUser notificationUser, CancellationToken cancellationToken = default);
        Task<NotificationUser> FindAsync(Guid notificationUserId, CancellationToken cancellationToken = default);


    }
}
