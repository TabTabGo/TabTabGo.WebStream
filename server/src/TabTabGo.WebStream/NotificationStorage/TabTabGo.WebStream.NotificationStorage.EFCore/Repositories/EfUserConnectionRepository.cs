using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TabTabGo.Core;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Module;
using TabTabGo.WebStream.NotificationStorage.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Repositories
{
    class EfUserConnectionRepository(DbContext context) : TabTabGo.Data.EF.Repositories.GenericRepository<UserConnection, Guid>(context), IUserConnectionRepository
    {
        public UserConnection FindByConnectionId(string connectionId)
        {
            return context.Set<UserConnection>().Where(s => s.ConnectionId.Equals(connectionId)).FirstOrDefault();
        }

        public Task<UserConnection> FindByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserConnection>().Where(s => s.ConnectionId.Equals(connectionId)).FirstOrDefaultAsync(cancellationToken);
        }

        public List<UserConnection> FindByUserId(string userId)
        {
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId)).ToList();
        }

        public Task<List<UserConnection>> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId)).ToListAsync(cancellationToken);
        }
    }
}
