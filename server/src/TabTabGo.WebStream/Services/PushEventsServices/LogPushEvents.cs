using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.PushEventsServices
{
    internal class LogPushEvents(ILogger<IPushEvent> logger) : IPushEvent
    {
        public Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("pushing message to devices of connections  {@connectionIds} {@message}", connectionIds, message);
            return Task.CompletedTask;
        }

        public Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("pushing message to device of connection {@connectionId} {@message}", connectionId, message);
            return Task.CompletedTask;
        }

        public Task PushToUserAsync(IEnumerable<UserIdData> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("pushing message to users of ids {@userIds} {@message}", userIds, message);
            return Task.CompletedTask;
        }

        public Task PushToUserAsync(UserIdData userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            logger.LogInformation("pushing message to user of Id {@userId} {@message}", userId, message);
            return Task.CompletedTask;
        }
    }
}
