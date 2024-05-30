using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.Services.Contract
{
    public interface IConnectionManager
    {
        void RegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null);
        void UnRegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null);
        void ReRegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null);
        Task RegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);
        Task UnRegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);
        Task ReRegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);

    }
}
