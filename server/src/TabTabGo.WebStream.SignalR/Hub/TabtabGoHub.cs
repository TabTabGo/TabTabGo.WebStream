using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.ReceivedEvent;

namespace TabTabGo.WebStream.SignalR.Hub
{

    public class WebStreamHub(
        IReceiveEvent eventHandler,
        IConnectionManager connectionManager,
        IUserConnections connections)
        : Microsoft.AspNetCore.SignalR.Hub
    {
        IReceiveEvent _eventHandler = eventHandler;

        public override async Task OnConnectedAsync()
        {
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
            var userConnections = await connections.GetUserConnectionIdsAsync(this.Context.UserIdentifier);
            if(!userConnections.Contains(this.Context.ConnectionId)) 
            {
                await connectionManager.RegisterConnectionAsync(this.Context.ConnectionId, this.Context.UserIdentifier, connectedInfo);
            }
            else
            {
                await connectionManager.ReRegisterConnectionAsync(this.Context.ConnectionId, this.Context.UserIdentifier);
            }     
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await connectionManager.UnRegisterConnectionAsync(this.Context.ConnectionId, this.Context.UserIdentifier);
        }
        public Task ClientEvent(SignalReceiveEvent webStreamMessage)
        {
            return eventHandler.OnEventReceived(this.Context.UserIdentifier, new WebStreamMessage(webStreamMessage.EventName, webStreamMessage.Data));
        }
    }
}
