
using Amqp;
using System.Text;
using System.Text.Json.Nodes;
using TabTabGo.WebStream.AMQP.Model;
using TabTabGo.WebStream.AMQP.Options;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.AMQP.Services
{
    public class TTGAMQPPushRedirect : IPushEvent
    {
        readonly AMQPOptions _options;
        //ConnectionFactory _factory;
        readonly Address _address;
        public TTGAMQPPushRedirect(AMQPOptions options)
        {
            _options = options;
            /* _factory = new ConnectionFactory()
             {
                 UserName = options.UserName,
                 Password = options.Password,
                 HostName = options.Host,
                 Port = options.Port,
             };*/ 
             _address = new Address($"amqp://{_options.UserName}:{_options.Password}@{_options.Host}:{_options.Port}/{(!string.IsNullOrWhiteSpace(_options.Vhost) ? _options.Vhost : "")}");
        }
        private async Task PushToAMQP(QueueMessage queueMessage)
        {

            var connection = await Connection.Factory.CreateAsync(_address);
            var session = new Session(connection);
            var link = new Amqp.SenderLink(session, "sender-link", _options.QueueName);
            var message = new Amqp.Message(Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(queueMessage))); 
            try
            { 
                await link.SendAsync(message);
            }
            catch
            {

                await link.CloseAsync();
                await session.CloseAsync();
                await connection.CloseAsync();
                throw;

            }
            await link.CloseAsync();
            await session.CloseAsync();
            await connection.CloseAsync(); 
        }

       /* private async Task PushToAMQP(QueueMessage queueMessage)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    var body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(queueMessage));

                    // Publish the message to the queue
                    channel.BasicPublish(exchange: "",
                                         routingKey: _options.QueueName,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }*/
        public Task PushAsync(IEnumerable<string> connectionIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return this.PushToAMQP(new QueueMessage() { ConnectionsId = connectionIds, WebStreamMessage = message });
        }

        public Task PushAsync(string connectionId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return this.PushToAMQP(new QueueMessage() { ConnectionsId = new List<string> { connectionId }, WebStreamMessage = message });
        }

        public Task PushToUserAsync(IEnumerable<UserIdData> userIds, WebStreamMessage message, CancellationToken cancellationToken = default)
        {
            return this.PushToAMQP(new QueueMessage() { UserIdData = userIds, WebStreamMessage = message });
        }

        public Task PushToUserAsync(UserIdData userId, WebStreamMessage message, CancellationToken cancellationToken = default)
        {

            return this.PushToAMQP(new QueueMessage() { UserIdData = new List<UserIdData> { userId }, WebStreamMessage = message });
        }
    }
}
