using System.Threading.Tasks;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services;

namespace Services
{
    /// <summary>
    /// this will used to ignore Events sent by Stream client
    /// </summary>
    public class NullReceiveEvent : IReceiveEvent
    {
        public Task OnEventReceived(string connectionId, WebStreamMessage message)
        { 
            return Task.CompletedTask;
        }
    }
}
