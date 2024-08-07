using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.Core.Data;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.Model; 
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.MessageStorage.Services
{
    /// <summary>
    /// it is the implemetation of IPushEvent  to save webstream messages  to database
    /// </summary>
    public class PushToStorageService(
        IUserConnections userConnections,
        IUnitOfWork unitOfWork,
        IMessageRepository messages,
        IUserStreamStorageMessageRepository users)
        : IPushEvent
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var userIds = await userConnections.GetUsersIdsByConnectionIdsAsync(connectionIds, cancellationToken);
            await this.Save(userIds, message, cancellationToken); 
        }

        public async Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var userId = await userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken);
            if (userId == null) return;
            await this.Save(userId, message, cancellationToken); 
        }
        public Task PushToUserAsync(IEnumerable<UserIdData> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Save(userIds, message, cancellationToken);
        }

        public Task PushToUserAsync(UserIdData userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            if (userId == null) return Task.CompletedTask ;
            return Save(userId, message, cancellationToken);
        }


        public async Task Save(IEnumerable<UserIdData> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {

            var dbMessage = await messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (dbMessage == null)
            {
                dbMessage = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection=Direction.Out
                };
                await messages.InsertAsync(dbMessage, cancellationToken);
            }

            foreach (var userId in userIds.Distinct().ToList())
            {

                var user = new UserWebStreamStorageMessage()
                {
                    SentDate = DateTime.UtcNow,
                    MessageId = dbMessage.Id,
                    UserId = userId.UserId,
                    TenantId = userId.TenantId,
                };
                await users.InsertAsync(user, cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Save(UserIdData userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            if (userId == null) return;
            var notification = await messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection = Direction.Out
                };
                await messages.InsertAsync(notification, cancellationToken);
            }
            var user = new UserWebStreamStorageMessage()
            {
                SentDate = DateTime.UtcNow,
                MessageId = notification.Id,
                UserId = userId.UserId,
                TenantId = userId.TenantId,
            };
            await users.InsertAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
