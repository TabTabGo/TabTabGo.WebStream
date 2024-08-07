using TabTabGo.Core.Data;
using TabTabGo.WebStream.MessageStorage.Entites;
using TabTabGo.WebStream.MessageStorage.Repository;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.MessageStorage.Services
{
    /// <summary>
    /// it is the implementation of IPushEvent and ISendNotification to send messages and notification to user and store in database
    /// </summary>
    public class PushToStorageOnSuccess(
        IPushEvent pushEvent,
        IUserConnections userConnections,
        IUnitOfWork unitOfWork,
        IMessageRepository messages,
        IUserStreamStorageMessageRepository users)
        : IPushEvent
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task PushAsync(IEnumerable<string> connectionIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {

            await pushEvent.PushAsync(connectionIds, message, cancellationToken);
            var userIds = await userConnections.GetUsersIdsByConnectionIdsAsync(connectionIds, cancellationToken);
            await this.PushToUserAsync(userIds, message, cancellationToken);
        }
        public async Task PushAsync(string connectionId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await pushEvent.PushAsync(connectionId, message, cancellationToken);
            var userId = await userConnections.GetUserIdByConnectionIdAsync(connectionId, cancellationToken);
            if (userId == null) return;
            await this.PushToUserAsync(userId, message, cancellationToken);
        }

        public async Task PushToUserAsync(IEnumerable<UserIdData> userIds, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            await pushEvent.PushToUserAsync(userIds, message, cancellationToken);

            var dbMessage = await messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (dbMessage == null)
            {
                dbMessage = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection = Direction.Out
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
                    TenantId = userId.TenantId

                };
                await users.InsertAsync(user, cancellationToken);
            }
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task PushToUserAsync(UserIdData userId, Model.WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            if (userId == null) return;
            await pushEvent.PushToUserAsync(userId, message, cancellationToken);

            var dbMessage = await messages.GetByKeyAsync(message.Id, cancellationToken: cancellationToken);
            if (dbMessage == null)
            {
                dbMessage = new Entites.WebStreamStorageMessage()
                {
                    Id = message.Id,
                    EventName = message.EventName,
                    Message = message.Data,
                    MessageDirection = Direction.Out
                };
                await messages.InsertAsync(dbMessage, cancellationToken);
            }
            var user = new UserWebStreamStorageMessage()
            {
                SentDate = DateTime.UtcNow,
                MessageId = dbMessage.Id,
                UserId = userId.UserId,
                TenantId = userId.TenantId
            };
            await users.InsertAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
        }


    }
}
