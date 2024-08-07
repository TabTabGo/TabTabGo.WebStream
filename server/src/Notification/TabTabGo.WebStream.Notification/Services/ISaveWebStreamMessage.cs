using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Notification.Services
{
    /// <summary>
    /// this interface used to store message in notification storage <br/>
    /// use it if you need to store WebStreamMessage without send to users
    /// </summary>
    public interface ISaveWebStreamMessage
    {
        Task Save(IEnumerable<UserIdData> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task Save(UserIdData userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default); 
    }
}
