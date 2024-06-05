using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.Hub;

namespace TabTabGo.WebStream.Services
{
    public class PushSignalREvent : IPushEvent
    {

        private readonly IHubContext<TabtabGoHub> _hubContext;
        private readonly IUserConnections _userConnections;
        public PushSignalREvent(IHubContext<TabtabGoHub> hubContext, IUserConnections userConnections)
        {
            _hubContext = hubContext;
            _userConnections = userConnections;
        }
        public Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients.Clients(connectionIds.ToList()).SendAsync(message.EventName, message.Data, cancellationToken);
        }
        public Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients.Client(connectionId).SendAsync(message.EventName, message.Data, cancellationToken);
        }
        public Task PushToUserAsync(IEnumerable<string> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var connectionsIds = _userConnections.GetUsersConnections(userIds);
            return _hubContext.Clients.Clients(connectionsIds).SendAsync(message.EventName, message.Data, cancellationToken);
        }
        public Task PushToUserAsync(string userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var connectionsIds = _userConnections.GetUserConnectionIds(userId);
            return _hubContext.Clients.Clients(connectionsIds).SendAsync(message.EventName, message.Data, cancellationToken);
        }
    }
}
