using TabTabGo.Core.Data;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.MessageStorage.Services
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

        public List<string> GetUserConnectionIds(UserIdData userId)
        {
            if (userId == null) return new List<string>();
            return _userConnectionRepository.GetByUserId(userId).Select(userConnection => userConnection.ConnectionId).ToList();
        }

        public async Task<List<string>> GetUserConnectionIdsAsync(UserIdData userId, CancellationToken cancellationToken = default)
        {
            if (userId == null) return new List<string>();
            return (await _userConnectionRepository.GetByUserIdAsync(userId, cancellationToken)).Select(userConnection => userConnection.ConnectionId).ToList();
        }

        public UserIdData GetUserIdByConnectionId(string connectionId)
        {
            var result = _userConnectionRepository.GetByConnectionId(connectionId);
            if (result == null) return null;
            return UserIdData.From(result.UserId, result.TenantId);
        }

        public async Task<UserIdData> GetUserIdByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default)
        {
            var result = (await _userConnectionRepository.GetByConnectionIdAsync(connectionId, cancellationToken));
            if (result == null) return null;
            return UserIdData.From(result.UserId, result.TenantId);
        }

        public List<string> GetUsersConnections(IEnumerable<UserIdData> userIds)
        {
            return userIds
                .SelectMany(userId => _userConnectionRepository.GetByUserId(userId))
                .Select(userConnection => userConnection.ConnectionId)
                .ToList();
        }

        public async Task<List<string>> GetUsersConnectionsAsync(IEnumerable<UserIdData> userIds, CancellationToken cancellationToken = default)
        {
            var connectionIds = await Task.WhenAll(userIds.Select(userId => _userConnectionRepository.GetByUserIdAsync(userId, cancellationToken)));
            return connectionIds.SelectMany(ids => ids).Select(c => c.ConnectionId).ToList();
        }

        public List<UserIdData> GetUsersIdsByConnectionIds(IEnumerable<string> connectionIds)
        {
            return connectionIds
               .Select(connectionId => _userConnectionRepository.GetByConnectionId(connectionId))
               .Select(userConnection => new UserIdData(userConnection.UserId, userConnection.TenantId))
               .ToList();
        }

        public async Task<List<UserIdData>> GetUsersIdsByConnectionIdsAsync(IEnumerable<string> connectionIds, CancellationToken cancellationToken = default)
        {
            var userConnections = await Task.WhenAll(connectionIds.Select(connectionId => _userConnectionRepository.GetByConnectionIdAsync(connectionId, cancellationToken)));
            return userConnections.Select(uc => new UserIdData(uc.UserId, uc.TenantId)).ToList();
        }
    }
}
