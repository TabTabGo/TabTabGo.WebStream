using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.NotificationStorage.Entites;
using TabTabGo.WebStream.NotificationStorage.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    /// <summary>
    /// it is the implemetation of IPushEvent and ISendNotification to send messages and notification to user and store in database
    /// </summary>
    public class PushToStorageSucessOnDecorator : IPushEvent, ISendNotification
    {
        private readonly IPushEvent _pushEvent;
        private readonly IUserConnections _userConnections;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepository _notifications;
        private readonly INotificationUserRepository _users;
        public PushToStorageSucessOnDecorator(IPushEvent pushEvent, IUserConnections userConnections, IUnitOfWork unitOfWork, INotificationRepository notifications, INotificationUserRepository users)
        {
            _pushEvent = pushEvent;
            _userConnections = userConnections;
            _unitOfWork = unitOfWork;
            _users = users;
            _notifications = notifications;
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {

            await _pushEvent.PushAsync(connectionIds, message, cancellationToken);
            var notification = await _notifications.GetByKeyAsync(message.NotificationId, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Notification()
                {
                    Id = message.NotificationId,
                    EventName = message.EventName,
                    Message = message.Data,
                }; await _notifications.InsertAsync(notification, cancellationToken);
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
                await _users.InsertAsync(user, cancellationToken);
            }
        }

        public async Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await _pushEvent.PushAsync(connectionId, message, cancellationToken);
            var notification = await _notifications.GetByKeyAsync(message.NotificationId, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Notification()
                {
                    Id = message.NotificationId,
                    EventName = message.EventName,
                    Message = message.Data,
                };
                await _notifications.InsertAsync(notification, cancellationToken);
            }

            var userId = await _userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken);
            var user = new NotificationUser()
            {
                NotifiedDateTime = DateTime.UtcNow,
                NotificationId = notification.Id,
                UserId = userId
            };
            await _users.InsertAsync(user, cancellationToken);


        } 

        public async Task PushToUserAsync(IEnumerable<string> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await _pushEvent.PushToUserAsync(userIds, message, cancellationToken);

            var notification = await _notifications.GetByKeyAsync(message.NotificationId, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Notification()
                {
                    Id = message.NotificationId,
                    EventName = message.EventName,
                    Message = message.Data,
                };
                await _notifications.InsertAsync(notification, cancellationToken);
            }

            foreach (var userId in userIds.Distinct().ToList())
            {

                var user = new NotificationUser()
                {
                    NotifiedDateTime = DateTime.UtcNow,
                    NotificationId = notification.Id,
                    UserId = userId
                };
                await _users.InsertAsync(user, cancellationToken);
            }

        }

        public async Task PushToUserAsync(string userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await _pushEvent.PushToUserAsync(userId, message, cancellationToken);

            var notification = await _notifications.GetByKeyAsync(message.NotificationId, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Notification()
                {
                    Id = message.NotificationId,
                    EventName = message.EventName,
                    Message = message.Data,
                };
                await _notifications.InsertAsync(notification, cancellationToken);
            }
            var user = new NotificationUser()
            {
                NotifiedDateTime = DateTime.UtcNow,
                NotificationId = notification.Id,
                UserId = userId
            };
            await _users.InsertAsync(user, cancellationToken);
        }

        public Task SendNotification(IEnumerable<string> userIds, object data, CancellationToken cancellationToken = default)
        {
           return this.PushToUserAsync(userIds, new WebStreamMessage(nameof(SendNotification), data), cancellationToken);
        } 
        public Task SendNotification(string userId, object data, CancellationToken cancellationToken = default)
        {
            return this.PushToUserAsync(userId, new WebStreamMessage(nameof(SendNotification), data), cancellationToken);
        }
    }
}
