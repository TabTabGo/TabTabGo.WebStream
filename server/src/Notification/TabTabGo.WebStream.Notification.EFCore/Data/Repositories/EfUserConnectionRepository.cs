using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using TabTabGo.Core;
using TabTabGo.Core.Models;
using TabTabGo.Core.Services;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Module;
using TabTabGo.WebStream.Notification.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TabTabGo.WebStream.Notification.EFCore.Repositories
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

        public List<UserConnection> GetByUserId(string userId)
        {
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId)).ToList();
        }

        public Task<List<UserConnection>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return context.Set<UserConnection>().Where(s => s.UserId.Equals(userId)).ToListAsync(cancellationToken);
        }
    }
}
