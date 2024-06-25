using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.Services
{
    public interface IUserConnectionRepository : TabTabGo.Core.Data.IGenericRepository<UserConnection, Guid>
    {
        List<UserConnection> GetByUserId(string userId);
        UserConnection GetByConnectionId(string connectionId);
        Task<List<UserConnection>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<UserConnection> GetByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default);
    }
}
