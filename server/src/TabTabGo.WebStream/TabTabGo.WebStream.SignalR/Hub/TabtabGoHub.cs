using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.RecivedEvent;

namespace TabTabGo.WebStream.SignalR.Hub
{
    public class TabtabGoHub : Microsoft.AspNetCore.SignalR.Hub
    {
        IReceiveEvent _eventHandler;
        IConnectionManager _connectionManager;
        IUserConnections _userConnections;
        public TabtabGoHub(IReceiveEvent eventHandler, IConnectionManager connectionManager, IUserConnections userConnections)
        {
            _eventHandler = eventHandler;
            _connectionManager = connectionManager;
            _userConnections = userConnections;
        }
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("hi");
            Console.WriteLine("hi");
            Console.WriteLine("hi");
            Console.WriteLine("hi");
            var userConnections = await _userConnections.GetUserConnectionIdsAsync(this.Context.UserIdentifier);
            if(!userConnections.Contains(this.Context.ConnectionId)) 
            {
                await _connectionManager.ReRegisterConnectionAsync(this.Context.ConnectionId, this.Context.UserIdentifier);

            }
            else
            {
                await _connectionManager.RegisterConnectionAsync(this.Context.ConnectionId, this.Context.UserIdentifier);
            }

               
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _connectionManager.UnRegisterConnectionAsync(this.Context.ConnectionId, this.Context.UserIdentifier);
        }
        public Task ClientEvent(SignalReceiveEvent webStreamMessage)
        {
            return _eventHandler.OnEventReceived(this.Context.UserIdentifier, new WebStreamMessage(webStreamMessage.EventName, webStreamMessage.Data));
        }
    }
}
