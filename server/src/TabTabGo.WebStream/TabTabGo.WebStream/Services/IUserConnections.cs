using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.Services
{
    public interface IUserConnections
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>List of user Connectin</returns>

        List<string> GetUserConnectionIds(string UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>User Id of this connection</returns>
        string GetUserIdByConnectionId(string connectionId);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>List of user Connectin</returns>

        Task<List<string>> GetUserConnectionIdsAsync(string UserId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns>User Id of this connection</returns>
        Task<string> GetUserIdByConnectionIdAsync(string connectionId, CancellationToken cancellationToken = default);



        List<string> GetUsersIdsByConnectionIds(IEnumerable<string> connectionIds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionIds"></param>
        /// <returns>List Users Ids </returns>
        Task<List<string>> GetUsersIdsByConnectionIdsAsync(IEnumerable<string> connectionIds,CancellationToken cancellationToken=default);

    }
}
