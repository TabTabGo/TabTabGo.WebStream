using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.Hub;

namespace TabTabGo.WebStream.SignalR.Services
{
    public class SignalRConnectionManager : IConnectionManager
    {
        private readonly IHubContext<TabtabGoHub> _hubContext;

        public SignalRConnectionManager(IHubContext<TabtabGoHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void RegisterConnection(string connectionId, string userId)
        {
            _hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You connected with connection ID: {connectionId}");
        }

        public async Task RegisterConnectionAsync(string connectionId, string userId, CancellationToken cancellationToken = default)
        {
            await _hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You connected with connection ID: {connectionId}");
        }

        public void ReRegisterConnection(string connectionId, string userId)
        {
            _hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You reconnected with connection ID: {connectionId}");
        }

        public async Task ReRegisterConnectionAsync(string connectionId, string userId, CancellationToken cancellationToken = default)
        {
            await _hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You reconnected with connection ID: {connectionId}");
        }

        public void UnRegisterConnection(string connectionId, string userId)
        {
            _hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You disconnected with connection ID: {connectionId}");
        }

        public async Task UnRegisterConnectionAsync(string connectionId, string userId, CancellationToken cancellationToken = default)
        {
           await _hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You disconnected with connection ID: {connectionId}");
        }
    }
}
