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
        private readonly INotificationUnitOfWorkFactory _notificationUnitofWorkFactory;
        public PushToStorageService(IUserConnections userConnections, INotificationUnitOfWorkFactory notificationUnitofWorkFactory)
        {
            _userConnections = userConnections;
            _notificationUnitofWorkFactory = notificationUnitofWorkFactory;
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
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
                        NotifiedDateTime = DateTime.UtcNow,
                        NotificationId = notificationId,
                        UserId = userId
                    };
                    await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                }
                transaction.Commit();
            }
        }
        public async Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
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
                    NotifiedDateTime = DateTime.UtcNow,
                    NotificationId = notificationId,
                    UserId = userId
                };
                await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                transaction.Commit();
            }

        }
        public Task PushAsync(IEnumerable<string> connectionIds, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return this.PushAsync(connectionIds, new WebStreamMessage { EventName = eventName, Data = data }, cancellationToken);
        }
        public Task PushAsync(string connectionId, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return this.PushAsync(connectionId, new WebStreamMessage { EventName = eventName, Data = data }, cancellationToken);
        }



        public async Task PushToUserAsync(IEnumerable<string> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
            {

                var notification = new Notification()
                {
                    EventName = message.EventName,
                    Message = message.Data,
                };
                var notificationId = await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
                foreach (var userId in userIds.Distinct().ToList())
                {

                    var user = new NotificationUser()
                    {
                        NotifiedDateTime = DateTime.UtcNow,
                        NotificationId = notificationId,
                        UserId = userId
                    };
                    await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                }
                transaction.Commit();
            }
        }

        public async Task PushToUserAsync(string userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
            {
                var notification = new Notification()
                {
                    EventName = message.EventName,
                    Message = message.Data,
                };
                var notificationId = await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
                var user = new NotificationUser()
                {
                    NotifiedDateTime = DateTime.UtcNow,
                    NotificationId = notificationId,
                    UserId = userId
                };
                await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                transaction.Commit();
            }
        }

        public Task PushToUserAsync(IEnumerable<string> userIds, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return this.PushAsync(userIds, new WebStreamMessage { EventName = eventName, Data = data }, cancellationToken);
        }

        public Task PushToUserAsync(string userId, string eventName, object data, CancellationToken cancellationToken = default)
        {
            return this.PushAsync(userId, new WebStreamMessage { EventName = eventName, Data = data }, cancellationToken);
        }
    }
}
