using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.Repository
{
    public interface IUserConnectionRepository : TabTabGo.Core.Data.IGenericRepository<UserConnection, Guid>
    {
        List<UserConnection> FindByUserId(string userId);
        UserConnection FindByConnectionId(string connectionId);
        Task<List<UserConnection>> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<UserConnection> FindByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default);
    }
}
