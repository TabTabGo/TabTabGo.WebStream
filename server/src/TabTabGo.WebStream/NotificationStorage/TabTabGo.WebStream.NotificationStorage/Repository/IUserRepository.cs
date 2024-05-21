using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface IUserRepository
    {
        string Create(NotificationUser user);
        void Update(NotificationUser user);
        NotificationUser Find(string userId);

        Task<string> CreateAsync(NotificationUser user, CancellationToken cancellationToken = default);
        Task UpdateAsync(NotificationUser user, CancellationToken cancellationToken = default);
        Task<NotificationUser> FindAsync(string userId, CancellationToken cancellationToken = default);

    }
}
