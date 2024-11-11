using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.PushEventsServices
{
    public class NullPushEvent : IPushEvent
    {
        public Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushAsync(IEnumerable<UserIdData> connectionIds, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushAsync(string connectionId, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushToUserAsync(IEnumerable<UserIdData> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushToUserAsync(UserIdData userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushToUserAsync(IEnumerable<UserIdData> userIds, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task PushToUserAsync(UserIdData userId, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
