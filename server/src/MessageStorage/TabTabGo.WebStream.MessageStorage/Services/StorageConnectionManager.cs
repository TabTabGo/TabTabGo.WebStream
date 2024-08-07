using TabTabGo.Core.Data;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.MessageStorage.Services
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

        public void RegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            _userConnectionRepository.Insert(new UserConnection
            {
                ConnectionId = connectionId,
                UserId = userId.UserId,
                TenantId = userId.TenantId, 
                ExtraProperties = parameters, 
                UpdatedBy = userId.UserId,
                CreatedBy= userId.UserId,
                UpdatedDate = DateTime.Now
            });
            _unitOfWork.SaveChanges();
        }

        public async Task RegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            await _userConnectionRepository.InsertAsync(new UserConnection
            {
                ConnectionId = connectionId,
                UserId = userId.UserId,
                TenantId= userId.TenantId,
                ExtraProperties = parameters,
                UpdatedBy= userId.UserId,
                UpdatedDate= DateTime.Now,
                CreatedBy= userId.UserId,
            }, cancellationToken);
            await _unitOfWork.SaveChangesAsync();

        }
        public void UnRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            _userConnectionRepository.Delete(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId.UserId && userConn.TenantId==userId.TenantId);
            _unitOfWork.SaveChanges();
        }

        public async Task UnRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            _userConnectionRepository.Delete(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId.UserId && userConn.TenantId == userId.TenantId);
            await _unitOfWork.SaveChangesAsync();
        }
        public void ReRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            var userConnection = _userConnectionRepository.FirstOrDefault(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId.UserId && userConn.TenantId == userId.TenantId);
            if (userConnection != null)
            {
                userConnection.ReConnectedDate = DateTime.Now;
                _userConnectionRepository.Update(userConnection);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task ReRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            var userConnection = await _userConnectionRepository.FirstOrDefaultAsync(filter: userConn => userConn.ConnectionId == connectionId && userConn.UserId == userId.UserId && userConn.TenantId == userId.TenantId, cancellationToken: cancellationToken);
            if (userConnection != null)
            {
                userConnection.ReConnectedDate = DateTime.Now;
                await _userConnectionRepository.UpdateAsync(userConnection, cancellationToken);
                await _unitOfWork.SaveChangesAsync();
            }
        }

    }
}
