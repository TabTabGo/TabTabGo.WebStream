using System;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface INotificationTransaction : IDisposable
    {
        void Commit();
        void RollBack();
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollBackAsync(CancellationToken cancellationToken = default);
    }
}
