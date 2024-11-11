using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlersServices
{
    /// <summary>
    /// this will used to ignore Events sent by Stream client
    /// </summary>
    public class NullReceiveEvent : IReceiveEvent
    {
        public Task OnEventReceived(UserIdData userId, WebStreamMessage message)
        {
            return Task.CompletedTask;
        }
    }
}
