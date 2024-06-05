using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    /// <summary>
    /// this interface used to store message in notification storage <br/>
    /// use it if you need to store WebStreamMessage without send to users
    /// </summary>
    public interface ISaveWebStreamMessage
    {
        Task Save(IEnumerable<string> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task Save(string userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default); 
    }
}
