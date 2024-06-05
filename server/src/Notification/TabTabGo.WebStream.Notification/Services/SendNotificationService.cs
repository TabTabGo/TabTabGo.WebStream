using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Notification.Services
{
    /// <summary>
    ///   send messages to user and store in notification database
    /// </summary>
    public class SendNotificationService(
        IPushEvent pushEvent,
        IUserConnections userConnections,
        IUnitOfWork unitOfWork,
        INotificationRepository notifications,
        INotificationUserRepository users)
        : ISendNotification
    {
        private readonly IUserConnections _userConnections = userConnections;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task SendNotification(IEnumerable<string> userIds, object data, CancellationToken cancellationToken = default)
        {
            var message = new WebStreamMessage(nameof(SendNotification), data);
            await pushEvent.PushToUserAsync(userIds, message, cancellationToken);
            var notification = await notifications.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Entities.NotificationMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                };
                await notifications.InsertAsync(notification, cancellationToken);
            }
            foreach (var userId in userIds.Distinct().ToList())
            {

                var user = new NotificationUser()
                {
                    NotifiedDateTime = DateTime.UtcNow,
                    NotificationId = notification.Id,
                    UserId = userId
                };
                await users.InsertAsync(user, cancellationToken);
            }
        }
        public async Task SendNotification(string userId, object data, CancellationToken cancellationToken = default)
        {
            var message = new WebStreamMessage(nameof(SendNotification), data);
            await pushEvent.PushToUserAsync(userId, message, cancellationToken);
            var notification = await notifications.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Entities.NotificationMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                };
                await notifications.InsertAsync(notification, cancellationToken);
            }
            var user = new NotificationUser()
            {
                NotifiedDateTime = DateTime.UtcNow,
                NotificationId = notification.Id,
                UserId = userId
            };
            await users.InsertAsync(user, cancellationToken);
        }
    }
}
