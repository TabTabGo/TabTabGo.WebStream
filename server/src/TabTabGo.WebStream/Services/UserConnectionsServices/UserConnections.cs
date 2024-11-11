using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
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

        public List<string> GetUserConnectionIds(UserIdData userId)
        {
            return _userConnections.FirstOrDefault().GetUserConnectionIds(userId);
        }

        public Task<List<string>> GetUserConnectionIdsAsync(UserIdData userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public UserIdData GetUserIdByConnectionId(string connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<UserIdData> GetUserIdByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUsersConnections(IEnumerable<UserIdData> userIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetUsersConnectionsAsync(IEnumerable<UserIdData> userIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<UserIdData> GetUsersIdsByConnectionIds(IEnumerable<string> connectionIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserIdData>> GetUsersIdsByConnectionIdsAsync(IEnumerable<string> connectionIds, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
