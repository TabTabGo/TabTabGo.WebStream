using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.Services.Contract
{
    public interface IConnectionManager
    {
        void RegisterConnection(string connectionId, string userId);
        void UnRegisterConnection(string connectionId, string userId);
        void ReRegisterConnection(string connectionId, string userId);
        Task RegisterConnectionAsync(string connectionId, string userId, CancellationToken cancellationToken = default);
        Task UnRegisterConnectionAsync(string connectionId, string userId, CancellationToken cancellationToken = default);
        Task ReRegisterConnectionAsync(string connectionId, string userId, CancellationToken cancellationToken = default);

    }
}
