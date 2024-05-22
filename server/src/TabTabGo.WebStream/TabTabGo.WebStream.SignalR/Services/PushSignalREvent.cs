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
        public PushSignalREvent(IHubContext<TabtabGoHub> hubContext)
        {
            _hubContext=hubContext;
        }
        public Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
           return _hubContext.Clients.Clients(connectionIds.ToList()).SendAsync(message.EventName,message.Data,cancellationToken);  
        }

        public Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients.Client(connectionId).SendAsync(message.EventName, message.Data, cancellationToken);
        }

        public Task PushAsync(IEnumerable<string> connectionIds, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients.Clients(connectionIds.ToList()).SendAsync(eventName, data, cancellationToken);
        }

        public Task PushAsync(string connectionId, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients.Client(connectionId).SendAsync(eventName, data, cancellationToken);
        }
    }
}
