using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.MessageStorage.Services
{
    public interface IUserConnectionRepository : TabTabGo.Core.Data.IGenericRepository<UserConnection, Guid>
    {
        List<UserConnection> GetByUserId(UserIdData userId);
        UserConnection GetByConnectionId(string connectionId);
        Task<List<UserConnection>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default);
        Task<UserConnection> GetByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default);
    }
}
