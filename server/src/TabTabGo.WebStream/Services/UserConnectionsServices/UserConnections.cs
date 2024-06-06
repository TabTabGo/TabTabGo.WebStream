using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.UserConnectionsServices
{
    public class UserConnections : IUserConnections
    {
        private readonly List<IUserConnections> _userConnections = null;

        public UserConnections(IEnumerable<IUserConnections> userConnections)
        {
            _userConnections = userConnections.ToList();
        }

        public List<string> GetUserConnectionIds(string UserId)
        {
            return _userConnections.FirstOrDefault().GetUserConnectionIds(UserId);
        }

        public Task<List<string>> GetUserConnectionIdsAsync(string UserId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdByConnectionId(string connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUsersConnections(IEnumerable<string> userIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetUsersConnectionsAsync(IEnumerable<string> userIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUsersIdsByConnectionIds(IEnumerable<string> connectionIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetUsersIdsByConnectionIdsAsync(IEnumerable<string> connectionIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
