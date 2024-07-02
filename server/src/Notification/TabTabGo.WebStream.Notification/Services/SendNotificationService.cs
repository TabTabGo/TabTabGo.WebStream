using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Notification.DTOs;
using TabTabGo.WebStream.Notification.Entities;
using TabTabGo.WebStream.Notification.Repository;
using TabTabGo.WebStream.Notification.Services;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.NotificationStorage.Services
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
        public async Task SendNotification(IEnumerable<string> userIds, object data,
            CancellationToken cancellationToken = default)
        {
            var currentDateTime = DateTime.UtcNow;
            var notification = new NotificationMessage()
            {
                Id = Guid.NewGuid(),
                Message = data,
            };
            var message = new WebStreamMessage(nameof(SendNotification), NotificationMessageDto.Map(notification, currentDateTime));
            notification.EventName = nameof(SendNotification);
            await pushEvent.PushToUserAsync(userIds, message, cancellationToken);

            await notifications.InsertAsync(notification, cancellationToken);

            foreach (var user in userIds.Distinct().ToList().Select(userId => new NotificationUser()
                     {
                         NotifiedDateTime = currentDateTime,
                         NotificationMessageId = notification.Id,
                         UserId = userId
                     }))
            {
                await users.InsertAsync(user, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task SendNotification(string userId, object data, CancellationToken cancellationToken = default)
        {
            var currentDateTime = DateTime.UtcNow;
            var notification = new NotificationMessage()
            {
                Id = Guid.NewGuid(),
                Message = data,
            };
            var message = new WebStreamMessage(nameof(SendNotification), NotificationMessageDto.Map(notification, currentDateTime));
            notification.EventName = nameof(SendNotification);
            await pushEvent.PushToUserAsync(userId, message, cancellationToken);
            await notifications.InsertAsync(notification, cancellationToken);
            

            var user = new NotificationUser()
            {
                NotifiedDateTime = currentDateTime,
                NotificationMessageId = notification.Id,
                UserId = userId
            };
            await users.InsertAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}