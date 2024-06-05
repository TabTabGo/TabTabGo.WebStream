using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.Hub;
using TabTabGo.WebStream.SignalR.Services;

namespace TabTabGo.WebStream.SignalR.Extensions.Builders
{
    public static class SignalRWebStreamBuilder
    {
        public static PushEventBuilder AddSignalR(this PushEventBuilder webStreamBuilder)
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushSignalREvent(serviceProvider.GetRequiredService<IHubContext<WebStreamHub>>(), serviceProvider.GetRequiredService<IUserConnections>()));
            return webStreamBuilder;
        }
    }
}
