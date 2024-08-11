using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.Hub;

namespace TabTabGo.WebStream.SignalR.Services
{
    public class SignalRConnectionManager<TUserKey, TTenantKey>(IHubContext<WebStreamHub<TUserKey,TTenantKey>> hubContext) : IConnectionManager where TUserKey : struct where TTenantKey : struct
    {
        public void RegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            hubContext.Clients.Client(connectionId).SendAsync(Constants.HandShakeMessage, $"You connected with connection ID: {connectionId}");
        }

        public async Task RegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            await hubContext.Clients.Client(connectionId).SendAsync(Constants.HandShakeMessage, $"You connected with connection ID: {connectionId}");
        }

        public void ReRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            hubContext.Clients.Client(connectionId).SendAsync(Constants.HandShakeMessage, $"You reconnected with connection ID: {connectionId}");
        }

        public async Task ReRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            await hubContext.Clients.Client(connectionId).SendAsync(Constants.HandShakeMessage, $"You reconnected with connection ID: {connectionId}");
        }

        public void UnRegisterConnection(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null)
        {
            hubContext.Clients.Client(connectionId).SendAsync(Constants.HandShakeMessage, $"You disconnected with connection ID: {connectionId}");
        }

        public async Task UnRegisterConnectionAsync(string connectionId, UserIdData userId, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
           await hubContext.Clients.Client(connectionId).SendAsync(Constants.HandShakeMessage, $"You disconnected with connection ID: {connectionId}");
        }
    }
}
