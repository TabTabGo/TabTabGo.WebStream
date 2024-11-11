using Amqp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;
using System.Text.Unicode;
using TabTabGo.WebStream.AMQP.Model;
using TabTabGo.WebStream.AMQP.Options;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.AMQP.Services
{
    public class TTHRedirectedMessagesConsumer : IHostedService, IDisposable
    {
        IServiceProvider _serviceProvider;
        readonly AMQPOptions _options;
        readonly Address _address;  
        Connection _connection;
        Session _session;
        ReceiverLink _receiverLink;
        public TTHRedirectedMessagesConsumer(IServiceProvider serviceProvider, AMQPOptions options)
        {
            _serviceProvider = serviceProvider;
            _options = options;
            _address = new Address($"amqp://{_options.UserName}:{_options.Password}@{_options.Host}:{_options.Port}/{(!string.IsNullOrWhiteSpace(_options.Vhost) ? _options.Vhost : "")}");
             


        }
        public void Dispose()
        {
            if (_receiverLink != null)
                _receiverLink.Close();
            if (_session != null)
                _session.Close();
            if (_connection != null)
                _connection.Close();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            _connection = Connection.Factory.CreateAsync(_address).GetAwaiter().GetResult();
            _session = new Session(_connection);
            _receiverLink = new ReceiverLink(_session, "receiver-link", _options.QueueName);
            return Task.Run(async () =>
             {
                 while (true)
                 {
                     var message = await _receiverLink.ReceiveAsync();
                     if (message != null)
                     {
                         var data = message.Body as byte[];
                         QueueMessage queueMessage = JsonConvert.DeserializeObject<QueueMessage>(Encoding.UTF8.GetString(data));
                         using (var scope = _serviceProvider.CreateAsyncScope())
                         {
                             var pushEvent = scope.ServiceProvider.GetRequiredService<IPushEvent>();
                             if (queueMessage.ConnectionsId != null && queueMessage.ConnectionsId.Any())
                                 await pushEvent.PushAsync(queueMessage.ConnectionsId, queueMessage.WebStreamMessage, cancellationToken);
                             if (queueMessage.UserIdData != null && queueMessage.UserIdData.Any())
                             {
                                 await pushEvent.PushToUserAsync(queueMessage.UserIdData, queueMessage.WebStreamMessage, cancellationToken);
                             }
                         }
                         _receiverLink.Accept(message);
                     }
                 }
             }, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            this.Dispose();
        }
    }
}
