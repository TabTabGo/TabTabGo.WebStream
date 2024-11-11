using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Services.Contract
{
    public interface IUserConnections
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of user Connection</returns>
        List<string> GetUserConnectionIds(UserIdData userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>User Id of this connection</returns>
        UserIdData GetUserIdByConnectionId(string connectionId);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of user connection</returns>

        Task<List<string>> GetUserConnectionIdsAsync(UserIdData userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>User id of this connection</returns>
        Task<UserIdData> GetUserIdByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionIds"></param>
        /// <returns></returns>
        List<UserIdData> GetUsersIdsByConnectionIds(IEnumerable<string> connectionIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>List Users Ids </returns>
        Task<List<UserIdData>> GetUsersIdsByConnectionIdsAsync(IEnumerable<string> connectionIds, CancellationToken cancellationToken = default);
        
        List<string> GetUsersConnections(IEnumerable<UserIdData> userIds);
        Task<List<string>> GetUsersConnectionsAsync(IEnumerable<UserIdData> userIds, CancellationToken cancellationToken = default);

    }
}
