using TabTabGo.Core.Data;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.MessageStorage.Services
{
    /// <summary>
    /// it is the implemetation of IPushEvent and ISendNotification to send messages and notification to user and store in database
    /// </summary>
    public class PushToStorageSucessOnDecorator : IPushEvent
    {
        private readonly IPushEvent _pushEvent;
        private readonly IUserConnections _userConnections;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messages;
        private readonly IUserStreamStorageMessageRepository _users;
        public PushToStorageSucessOnDecorator(IPushEvent pushEvent, IUserConnections userConnections, IUnitOfWork unitOfWork, IMessageRepository messages, IUserStreamStorageMessageRepository users)
        {
            _pushEvent = pushEvent;
            _userConnections = userConnections;
            _unitOfWork = unitOfWork;
            _users = users;
            _messages = messages;
        }
        public async Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {

            await _pushEvent.PushAsync(connectionIds, message, cancellationToken);
            var userIds = await _userConnections.GetUsersIdsByConnectionIdsAsync(connectionIds, cancellationToken);
            await this.PushToUserAsync(connectionIds, message, cancellationToken); 
        } 
        public async Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await _pushEvent.PushAsync(connectionId, message, cancellationToken);
            var userId = await _userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken); 
            await this.PushToUserAsync(userId, message, cancellationToken);  
        }

        public async Task PushToUserAsync(IEnumerable<string> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await _pushEvent.PushToUserAsync(userIds, message, cancellationToken);

            var dbMessage = await _messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (dbMessage == null)
            {
                dbMessage = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection = Direction.Out
                };
                await _messages.InsertAsync(dbMessage, cancellationToken);
            }

            foreach (var userId in userIds.Distinct().ToList())
            {

                var user = new UserWebStreamStorageMessage()
                {
                    SentDate = DateTime.UtcNow,
                    MessageId = dbMessage.Id,
                    UserId = userId,

                };
                await _users.InsertAsync(user, cancellationToken);
            }

        }

        public async Task PushToUserAsync(string userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await _pushEvent.PushToUserAsync(userId, message, cancellationToken);

            var dbMessage = await _messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (dbMessage == null)
            {
                dbMessage = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection = Direction.Out
                };
                await _messages.InsertAsync(dbMessage, cancellationToken);
            }
            var user = new UserWebStreamStorageMessage()
            {
                SentDate = DateTime.UtcNow,
                MessageId = dbMessage.Id,
                UserId = userId
            };
            await _users.InsertAsync(user, cancellationToken);
        }


    }
}
