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
    public class PushToStorageSucessOnDecorator : IPushEvent
    {
        private readonly IPushEvent _pushEvent;
        private readonly IUserConnections _userConnections;
        private readonly INotificationUnitOfWorkFactory _notificationUnitofWorkFactory;
        public PushToStorageSucessOnDecorator(IPushEvent pushEvent, IUserConnections userConnections, INotificationUnitOfWorkFactory notificationUnitofWorkFactory)
        {
            _pushEvent = pushEvent;
            _userConnections = userConnections;
            _notificationUnitofWorkFactory = notificationUnitofWorkFactory;
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
            {

                var notification = await _notificationUnitofWork.Notifications.FindAsync(message.NotificationId, cancellationToken);
                if (notification == null)
                {
                    notification = new Notification()
                    {
                        Id = message.NotificationId,
                        EventName = message.EventName,
                        Message = message.Data,
                    };
                    await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
                }
                var userIds = await _userConnections.GetUsersIdsByConnectionIdsAsync(connectionIds, cancellationToken);
                foreach (var userId in userIds.Distinct().ToList())
                {

                    var user = new NotificationUser()
                    {
                        NotifiedDateTime = DateTime.UtcNow,
                        NotificationId = notification.Id,
                        UserId = userId
                    };
                    await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                }
                await _pushEvent.PushAsync(connectionIds, message, cancellationToken);
                transaction.Commit();
            }
        }
        public async Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
            {

                var notification = await _notificationUnitofWork.Notifications.FindAsync(message.NotificationId, cancellationToken);
                if (notification == null)
                {
                    notification = new Notification()
                    {
                        Id = message.NotificationId,
                        EventName = message.EventName,
                        Message = message.Data,
                    }; await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
                }
                var userId = await _userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken);
                var user = new NotificationUser()
                {
                    NotifiedDateTime = DateTime.UtcNow,
                    NotificationId = notification.Id,
                    UserId = userId
                };
                await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                await _pushEvent.PushAsync(connectionId, message, cancellationToken);
                transaction.Commit();
            }

        }
        public async Task PushToUserAsync(IEnumerable<string> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
            {

                var notification = await _notificationUnitofWork.Notifications.FindAsync(message.NotificationId, cancellationToken);
                if (notification == null)
                {
                    notification = new Notification()
                    {
                        Id = message.NotificationId,
                        EventName = message.EventName,
                        Message = message.Data,
                    };
                    await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
                }
                foreach (var userId in userIds.Distinct().ToList())
                {

                    var user = new NotificationUser()
                    {
                        NotifiedDateTime = DateTime.UtcNow,
                        NotificationId = notification.Id,
                        UserId = userId
                    };
                    await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                }
                await _pushEvent.PushToUserAsync(userIds, message, cancellationToken);
                transaction.Commit();
            }
        }

        public async Task PushToUserAsync(string userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            using (var _notificationUnitofWork = _notificationUnitofWorkFactory.Get())
            using (var transaction = _notificationUnitofWork.StartTransaction())
            {

                var notification = await _notificationUnitofWork.Notifications.FindAsync(message.NotificationId, cancellationToken);
                if (notification == null)
                {
                    notification = new Notification()
                    {
                        Id = message.NotificationId,
                        EventName = message.EventName,
                        Message = message.Data,
                    }; await _notificationUnitofWork.Notifications.CreateAsync(notification, cancellationToken);
                }
                var user = new NotificationUser()
                {
                    NotifiedDateTime = DateTime.UtcNow,
                    NotificationId = notification.Id,
                    UserId = userId
                };
                await _notificationUnitofWork.UserRepository.CreateAsync(user, cancellationToken);
                await _pushEvent.PushToUserAsync(userId, message, cancellationToken);
                transaction.Commit();
            }
        }
    }
}
