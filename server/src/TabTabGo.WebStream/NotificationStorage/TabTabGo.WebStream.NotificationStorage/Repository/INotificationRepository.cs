using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationRepository
    {
        Guid Create(Notification notification);
        Notification Find(Guid Id);
        List<Notification> FindByUserId(string UserId); 
        Task<Guid> CreateAsync(Notification notification, CancellationToken cancellationToken = default);
        Task<Notification> FindAsync(Guid Id, CancellationToken cancellationToken = default);
        Task<List<Notification>> FindByUserIdAsync(string UserId, CancellationToken cancellationToken = default); 
    }
}
