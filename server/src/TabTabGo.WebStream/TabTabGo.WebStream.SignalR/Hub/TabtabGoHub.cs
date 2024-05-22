using Microsoft.AspNetCore.SignalR;
using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.SignalR.Hub
{
    public class TabtabGoHub : Microsoft.AspNetCore.SignalR.Hub
    {
        IReceiveEvent _eventHandler;
        public TabtabGoHub(IReceiveEvent eventHandler)
        {
            _eventHandler=eventHandler;
        }
        public override Task OnConnectedAsync()
        {
            //farah     
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            //farah 
            return base.OnDisconnectedAsync(exception);
        } 
        public Task ClientEvent(WebStreamMessage webStreamMessage)
        {
            return _eventHandler.OnEventReceived(this.Context.ConnectionId, webStreamMessage);
        } 
    }
}
