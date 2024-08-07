using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Services;
using TabTabGo.WebStream.Model;

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
            if (userId == null) return null;
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToList();
        }

        public Task<List<UserConnection>> GetByUserIdAsync(UserIdData userId, CancellationToken cancellationToken = default)
        {
            if (userId == null) return Task.FromResult( new  List<UserConnection>());
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId.UserId) && s.TenantId.Equals(userId.TenantId)).ToListAsync(cancellationToken);
        }
    }
}
