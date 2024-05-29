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
    public class PushToStorageService : IPushEvent
    {
        private readonly IUserConnections _userConnections;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messages;
        private readonly IUserStreamStorageMessageRepository _users; 
        public PushToStorageService(IUserConnections userConnections, IUnitOfWork unitOfWork, IMessageRepository messages, IUserStreamStorageMessageRepository users)
        {
            _userConnections = userConnections;
            _unitOfWork = unitOfWork;
            _users = users;
            _messages = messages;
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var userIds = await _userConnections.GetUsersIdsByConnectionIdsAsync(connectionIds, cancellationToken);
            await this.Save(userIds, message, cancellationToken); 
        }

        public async Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var userId = await _userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken);
            await this.Save(userId, message, cancellationToken); 
        }
        public Task PushToUserAsync(IEnumerable<string> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Save(userIds, message, cancellationToken);
        }

        public Task PushToUserAsync(string userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return Save(userId, message, cancellationToken);
        }


        public async Task Save(IEnumerable<string> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {

            var dbMessage = await _messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (dbMessage == null)
            {
                dbMessage = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection=Direction.Out
                };
                await _messages.InsertAsync(dbMessage, cancellationToken);
            }

            foreach (var userId in userIds.Distinct().ToList())
            {

                var user = new UserWebStreamStorageMessage()
                {
                    SentDate = DateTime.UtcNow,
                    MessageId = dbMessage.Id,
                    UserId = userId
                };
                await _users.InsertAsync(user, cancellationToken);
            }
        }

        public async Task Save(string userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            var notification = await _messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (notification == null)
            {
                notification = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection = Direction.Out
                };
                await _messages.InsertAsync(notification, cancellationToken);
            }
            var user = new UserWebStreamStorageMessage()
            {
                SentDate = DateTime.UtcNow,
                MessageId = notification.Id,
                UserId = userId
            };
            await _users.InsertAsync(user, cancellationToken);

        }
    }
}
