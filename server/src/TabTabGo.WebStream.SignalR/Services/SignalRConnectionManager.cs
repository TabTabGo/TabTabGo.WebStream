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
    public class SignalRConnectionManager(IHubContext<WebStreamHub> hubContext) : IConnectionManager
    {
        public void RegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null)
        {
            hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You connected with connection ID: {connectionId}");
        }

        public async Task RegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            await hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You connected with connection ID: {connectionId}");
        }

        public void ReRegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null)
        {
            hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You reconnected with connection ID: {connectionId}");
        }

        public async Task ReRegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            await hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You reconnected with connection ID: {connectionId}");
        }

        public void UnRegisterConnection(string connectionId, string userId, IDictionary<string, object>? parameters = null)
        {
            hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You disconnected with connection ID: {connectionId}");
        }

        public async Task UnRegisterConnectionAsync(string connectionId, string userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
           await hubContext.Clients.User(userId).SendAsync(Constants.HandShakeMessage, $"You disconnected with connection ID: {connectionId}");
        }
    }
}
