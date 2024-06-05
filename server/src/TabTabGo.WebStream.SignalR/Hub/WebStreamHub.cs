using System;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.ReceivedEvent;

namespace TabTabGo.WebStream.SignalR.Hub
{
    public class WebStreamHub(IReceiveEvent eventHandler) : Microsoft.AspNetCore.SignalR.Hub
    {
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
            return eventHandler.OnEventReceived(this.Context.UserIdentifier, new WebStreamMessage(webStreamMessage.EventName, webStreamMessage.Data));
        }
    }
}
