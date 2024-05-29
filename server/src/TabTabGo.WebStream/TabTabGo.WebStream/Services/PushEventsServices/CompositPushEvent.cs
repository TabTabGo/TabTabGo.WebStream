﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.PushEventsServices
{
    public class CompositPushEvent : IPushEvent
    {
        private readonly List<IPushEvent> _pushEvents = null;
        public CompositPushEvent(IEnumerable<IPushEvent> pushEvents)
        {
            _pushEvents = pushEvents.ToList();
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            foreach (var s in _pushEvents)
            {
                await s.PushAsync(connectionIds, message, cancellationToken);
            }
        }
        public async Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            foreach (var s in _pushEvents)
            {
                await s.PushAsync(connectionId, message, cancellationToken);
            }

        }
       


        public async Task PushToUserAsync(IEnumerable<string> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            foreach (var s in _pushEvents)
            {
                await s.PushToUserAsync(userIds, message, cancellationToken);
            }
        }

        public async Task PushToUserAsync(string UserId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            foreach (var s in _pushEvents)
            {
                await s.PushToUserAsync(UserId, message, cancellationToken);
            }
        } 
    }
}
