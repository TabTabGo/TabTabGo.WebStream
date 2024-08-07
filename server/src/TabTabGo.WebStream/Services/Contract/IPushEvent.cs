using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Services.Contract
{
    public interface IPushEvent
    {
        Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default); 
        Task PushToUserAsync(IEnumerable<UserIdData> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task PushToUserAsync(UserIdData userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default); 
    }
}
