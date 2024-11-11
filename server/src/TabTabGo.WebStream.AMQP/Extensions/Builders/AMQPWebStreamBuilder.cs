

using Microsoft.Extensions.DependencyInjection;
using TabTabGo.WebStream.AMQP.Options;
using TabTabGo.WebStream.AMQP.Services;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Builders.PushEventBuilders;

namespace TabTabGo.WebStream.AMQP.Extensions.Builders
{
    public static class AMQPWebStreamBuilder
    {
        public static PushEventBuilder AddAMPQRedirect(this PushEventBuilder webStreamBuilder, AMQPOptions options)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new TTGAMQPPushRedirect(options));
            return webStreamBuilder;
        }
        public static PushEventBuilder AddAMPQRedirect(this PushEventBuilder webStreamBuilder, Action<AMQPOptions> action)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) =>
            {
                AMQPOptions options = new AMQPOptions(); 
                action(options);
                return new TTGAMQPPushRedirect(options);
            });
            return webStreamBuilder;
        }  

        public static IServiceCollection Add_TTG_AMPQ_RedirectedEventsConsumer(this IServiceCollection services, AMQPOptions options)
        {
            services.AddHostedService(i => new TTHRedirectedMessagesConsumer(i, options));
            return services;
        }


    }
}
