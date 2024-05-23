using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.Services.Contract
{
    public interface IPushEvent
    {
        Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushAsync(IEnumerable<string> connectionIds, string eventName, object data, CancellationToken cancellationToken = default);
        Task PushAsync(string connectionId, string eventName, object data, CancellationToken cancellationToken = default);
         

        Task PushToUserAsync(IEnumerable<string> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushToUserAsync(string UserId, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushToUserAsync(IEnumerable<string> userIds, string eventName, object data, CancellationToken cancellationToken = default);
        Task PushToUserAsync(string UserId, string eventName, object data, CancellationToken cancellationToken = default);

    }
}
