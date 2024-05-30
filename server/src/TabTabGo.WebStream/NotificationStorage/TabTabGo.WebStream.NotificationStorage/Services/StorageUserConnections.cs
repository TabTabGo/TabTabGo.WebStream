using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    public class StorageUserConnections : IUserConnections
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserConnectionRepository _userConnectionRepository;

        public StorageUserConnections(IUnitOfWork unitOfWork, IUserConnectionRepository userConnectionRepository)
        {
            _unitOfWork = unitOfWork;
            _userConnectionRepository = userConnectionRepository;
        }

        public List<string> GetUserConnectionIds(string UserId)
        {
            return _userConnectionRepository.FindByUserId(UserId).Select(userConnection => userConnection.ConnectionId).ToList();
        }

        public async Task<List<string>> GetUserConnectionIdsAsync(string UserId, CancellationToken cancellationToken = default)
        {
            return (await _userConnectionRepository.FindByUserIdAsync(UserId)).Select(userConnection => userConnection.ConnectionId).ToList();
        }

        public string GetUserIdByConnectionId(string connectionId)
        {
            return _userConnectionRepository.FindByConnectionId(connectionId).UserId;
        }

        public async Task<string> GetUserIdByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default)
        {
            return _userConnectionRepository.FindByConnectionId(connectionId).UserId;
        }

        public List<string> GetUsersConnections(IEnumerable<string> userIds)
        {
            return userIds
                .SelectMany(userId => _userConnectionRepository.FindByUserId(userId))
                .Select(userConnection => userConnection.ConnectionId)
                .ToList();
        }

        public async Task<List<string>> GetUsersConnectionsAsync(IEnumerable<string> userIds, CancellationToken cancellationToken = default)
        {
            var connectionIds = await Task.WhenAll(userIds.Select(userId => _userConnectionRepository.FindByUserIdAsync(userId, cancellationToken)));
            return connectionIds.SelectMany(ids => ids).Select(c => c.ConnectionId).ToList();
        }

        public List<string> GetUsersIdsByConnectionIds(IEnumerable<string> connectionIds)
        {
            return connectionIds
               .Select(connectionId => _userConnectionRepository.FindByConnectionId(connectionId))
               .Select(userConnection => userConnection.UserId)
               .ToList();
        }

        public async Task<List<string>> GetUsersIdsByConnectionIdsAsync(IEnumerable<string> connectionIds, CancellationToken cancellationToken = default)
        {
            var userConnections = await Task.WhenAll(connectionIds.Select(connectionId => _userConnectionRepository.FindByConnectionIdAsync(connectionId, cancellationToken)));
            return userConnections.Select(uc => uc.UserId).ToList();
        }
    }
}
