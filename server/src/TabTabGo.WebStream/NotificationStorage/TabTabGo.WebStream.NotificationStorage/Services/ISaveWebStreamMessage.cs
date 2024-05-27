using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    public interface ISaveWebStreamMessage
    {
        Task Save(IEnumerable<string> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default);
        Task Save(string userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default); 
    }
}
