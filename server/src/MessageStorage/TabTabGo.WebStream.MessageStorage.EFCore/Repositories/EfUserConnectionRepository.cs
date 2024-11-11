using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TabTabGo.Core;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Services;
using TabTabGo.WebStream.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TabTabGo.WebStream.MessageStorage.EFCore.Repositories
{
    class EfUserConnectionRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<UserConnection, Guid>(context), IUserConnectionRepository
    {
        public UserConnection GetByConnectionId(string connectionId)
        {
            return context.Set<UserConnection>().Where(s => s.ConnectionId.Equals(connectionId)).FirstOrDefault();
        }

        public Task<UserConnection> GetByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserConnection>().Where(s => s.ConnectionId.Equals(connectionId)).FirstOrDefaultAsync(cancellationToken);
        }

        public List<UserConnection> GetByUserId(UserIdData userId)
        {
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToList();
        }

        public Task<List<UserConnection>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToListAsync(cancellationToken);
        }
    }
}
