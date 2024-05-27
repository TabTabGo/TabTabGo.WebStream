using System;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.RecivedEvent;

namespace TabTabGo.WebStream.SignalR.Hub
{
    public class TabtabGoHub : Microsoft.AspNetCore.SignalR.Hub
    {
        IReceiveEvent _eventHandler;
        public TabtabGoHub(IReceiveEvent eventHandler)
        {
            _eventHandler = eventHandler;
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
        public Task ClientEvent(SignalReceiveEvent webStreamMessage)
        {
            return _eventHandler.OnEventReceived(this.Context.UserIdentifier, new WebStreamMessage(webStreamMessage.EventName, webStreamMessage.Data));
        }
    }
}
