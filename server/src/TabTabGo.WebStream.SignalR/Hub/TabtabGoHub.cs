using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.ReceivedEvent;

namespace TabTabGo.WebStream.SignalR.Hub
{

    public class WebStreamHub<TUserKey, TTenantKey>(
        IReceiveEvent eventHandler,
        IConnectionManager connectionManager,
        TabTabGo.Core.Services.ISecurityService<TUserKey, TTenantKey> securityService,
        IUserConnections connections)
        : Microsoft.AspNetCore.SignalR.Hub where TUserKey : struct where TTenantKey : struct
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            // get relative client info from headers
            var host = Context.GetHttpContext().Request.Headers["Host"];
            var userAgent = Context.GetHttpContext().Request.Headers["User-Agent"];
            var realIP = Context.GetHttpContext().Request.Headers["X-Real-IP"];
            var forwardeds = Context.GetHttpContext().Request.Headers["X-Forwarded-For"];
            var connectedInfo = new Dictionary<string, object>()
            {
                {"Host", host},
                {"UserAgent", userAgent},
                {"Real-IP", realIP},
                {"Forward-For", forwardeds},
            }; 
            var userConnections = await connections.GetUserConnectionIdsAsync(this.GetUserId());
            if (!userConnections.Contains(this.Context.ConnectionId))
            {
                await connectionManager.RegisterConnectionAsync(this.Context.ConnectionId, this.GetUserId(), connectedInfo);
            }
            else
            {
                await connectionManager.ReRegisterConnectionAsync(this.Context.ConnectionId, this.GetUserId());
            }
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        { 
            await connectionManager.UnRegisterConnectionAsync(this.Context.ConnectionId, this.GetUserId()); 
        }
        public Task ClientEvent(SignalReceiveEvent webStreamMessage)
        { 
            return eventHandler.OnEventReceived(this.GetUserId(), new WebStreamMessage(webStreamMessage.EventName, webStreamMessage.Data));
        } 
        private UserIdData GetUserId()
        {
            var userId = securityService?.GetUserId().ToString();
            var tenantId = securityService?.GetTenantId().ToString();
            return new UserIdData(userId, tenantId);
        }
    }
}
