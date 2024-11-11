using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Services.Contract
{
    public interface IConnectionManager
    {
        void RegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null);
        void UnRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null);
        void ReRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null);
        Task RegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);
        Task UnRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);
        Task ReRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);

    }
}
