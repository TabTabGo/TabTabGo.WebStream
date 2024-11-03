namespace TabTabGo.WebStream.AMQP.Options
{
    public class AMQPOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Vhost { get; set; }
        public string QueueName { get; set; }
    }
}
