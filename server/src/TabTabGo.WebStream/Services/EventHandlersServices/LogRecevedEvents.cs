using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlersServices
{
    internal class LogRecevedEvents(ILogger<IReceiveEvent> logger) : IReceiveEvent
    {
        public Task OnEventReceived(string userId, WebStreamMessage message)
        {
            logger.LogTrace("the user of id {userId}, just sent a message {@message}",userId,message);
            return Task.CompletedTask;
        }
    }
}
