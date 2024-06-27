using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.Hub;

namespace TabTabGo.WebStream.SignalR.Services
{
    public class PushSignalREvent<TUserKey, TTenantKey>(IHubContext<WebStreamHub<TUserKey,TTenantKey>> hubContext, IUserConnections userConnections) 
        : IPushEvent where TUserKey : struct where TTenantKey : struct
    {
        public Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return hubContext.Clients.Clients(connectionIds.ToList()).SendAsync(message.EventName, message.Data, cancellationToken);
        }
        public Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return hubContext.Clients.Client(connectionId).SendAsync(message.EventName, message.Data, cancellationToken);
        }
        public Task PushToUserAsync(IEnumerable<string> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var connectionsIds = userConnections.GetUsersConnections(userIds);
            return hubContext.Clients.Clients(connectionsIds).SendAsync(message.EventName, message.Data, cancellationToken);
        }
        public Task PushToUserAsync(string userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var connectionsIds = userConnections.GetUserConnectionIds(userId);
            return hubContext.Clients.Clients(connectionsIds).SendAsync(message.EventName, message.Data, cancellationToken);
        }
    }
}
