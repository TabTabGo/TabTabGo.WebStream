using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.Services;
using TabTabGo.WebStream.SignalR.Hub;

namespace TabTabGo.WebStream.Builders
{
    public static class SignalRWebStreamBuilder
    {
        public static PushEventBuilder UseSignalR(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.SetIPushEvent((serviceProvider) => new PushSignalREvent(serviceProvider.GetRequiredService<IHubContext<TabtabGoHub>>()));
            return webStreamBuilder;
        }
    }
}
