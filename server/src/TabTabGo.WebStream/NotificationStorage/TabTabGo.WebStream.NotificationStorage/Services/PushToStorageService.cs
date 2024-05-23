using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    public class PushToStorageService : IPushEvent
    {
        private readonly IUserConnections _userConnections;
        private readonly INotificationUnitOfWork _notificationUnitofWork;
        public PushToStorageService(IUserConnections userConnections, INotificationUnitOfWork notificationUnitofWork)
        {
            _userConnections = userConnections;
            _notificationUnitofWork=notificationUnitofWork;
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var notification = new Notification()
            {
                EventName = message.EventName,
                Message = message.Data,
            };
            var notificationId = await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken); 
            var userIds = await _userConnections.GetUsersIdsByConnectionIdsAsync(connectionIds, cancellationToken);
            foreach (var userId in userIds.Distinct().ToList())
            {

                var user = new NotificationUser()
                {
                    NotidyDateTime = DateTime.UtcNow,
                    NotificationId = notificationId,
                    UserId = userId
                };
                await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
            }
        }
        public async Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var notification = new Notification()
            {
                EventName = message.EventName,
                Message = message.Data,
            };
            var notificationId = await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
            var userId = await _userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken);
            var user = new NotificationUser()
            {
                NotidyDateTime = DateTime.UtcNow,
                NotificationId = notificationId,
                UserId = userId
            };
            await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);

        }
        public Task PushAsync(IEnumerable<string> connectionIds, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return this.PushAsync(connectionIds, new WebStreamMessage { EventName = eventName, Data = data }, cancellationToken);
        }
        public Task PushAsync(string connectionId, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return this.PushAsync(connectionId, new WebStreamMessage { EventName = eventName, Data = data }, cancellationToken);
        }
    }
}
