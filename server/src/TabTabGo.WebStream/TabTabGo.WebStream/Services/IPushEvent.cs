using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.Services
{
    public interface IPushEvent
    {
        Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushAsync(IEnumerable<string> connectionIds, string eventName, object data, CancellationToken cancellationToken = default);
        Task PushAsync(string connectionId, string eventName, object data, CancellationToken cancellationToken = default);
    }
}
