using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services; 
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Services
{
    /// <summary>
    ///   send messages to user and store in notification database
    /// </summary>
    public class SendNotificationService : ISendNotification
    {
        private readonly IPushEvent _pushEvent;
        private readonly IUserConnections _userConnections;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepository _notifications;
        private readonly INotificationUserRepository _users;
        public SendNotificationService(IPushEvent pushEvent, IUserConnections userConnections, IUnitOfWork unitOfWork, INotificationRepository notifications, INotificationUserRepository users)
        {
            _pushEvent = pushEvent;
            _userConnections = userConnections;
            _unitOfWork = unitOfWork;
            _users = users;
            _notifications = notifications;
        }
        public async Task SendNotification(IEnumerable<string> userIds, object data, CancellationToken cancellationToken = default)
        {
            var message = new WebStreamMessage(nameof(SendNotification), data);
            await _pushEvent.PushToUserAsync(userIds, message, cancellationToken);
            var notification = await _notifications.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new NotificationMessage()
                {
                    Id = message.Id,
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
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task SendNotification(string userId, object data, CancellationToken cancellationToken = default)
        {
            var message = new WebStreamMessage(nameof(SendNotification), data);
            await _pushEvent.PushToUserAsync(userId, message, cancellationToken);
            var notification = await _notifications.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new NotificationMessage()
                {
                    Id = message.Id,
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
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
