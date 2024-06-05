using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    public class StorageConnectionManager : IConnectionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserConnectionRepository _userConnectionRepository;

        public StorageConnectionManager(IUnitOfWork unitOfWork, IUserConnectionRepository userConnectionRepository)
        {
            _unitOfWork = unitOfWork;
            _userConnectionRepository = userConnectionRepository;
        }

        public void RegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null)
        {
            _userConnectionRepository.Insert(new UserConnection
            {
                ConnectionId = connectionId,
                UserId = userId,
                ExtraProperties = parameters
            });
        }

        public async Task RegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            await _userConnectionRepository.InsertAsync(new UserConnection
            {
                ConnectionId = connectionId,
                UserId = userId,
                ExtraProperties = parameters
            }, cancellationToken);

        }
        public void UnRegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null)
        {
            _userConnectionRepository.Delete(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId);
        }

        public async Task UnRegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            _userConnectionRepository.Delete(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId);
        }
        public void ReRegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null)
        {
            var userConnection = _userConnectionRepository.FirstOrDefault(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId);
            if (userConnection != null)
            {
                userConnection.ReConnectedDate = DateTime.Now;
                _userConnectionRepository.Update(userConnection);
            }
        }

        public async Task ReRegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            var userConnection = await _userConnectionRepository.FirstOrDefaultAsync(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId, cancellationToken: cancellationToken);
            if (userConnection != null)
            {
                userConnection.ReConnectedDate = DateTime.Now;
                await _userConnectionRepository.UpdateAsync(userConnection, cancellationToken);
            }
        }

    }
}
