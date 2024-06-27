using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using TabTabGo.WebStream.Builders.ConnectionMangerBuilders;
using TabTabGo.WebStream.Builders.PushEventBuilders;
using TabTabGo.WebStream.Builders.UserConnectionsBuilders;
using TabTabGo.WebStream.Services;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.SignalR.Hub;
using TabTabGo.WebStream.SignalR.Services;

namespace TabTabGo.WebStream.SignalR.Extensions.Builders
{
    public static class SignalRWebStreamBuilder
    {
        public static PushEventBuilder AddSignalR<TUserKey, TTenantKey>(this PushEventBuilder webStreamBuilder) where TUserKey : struct where TTenantKey : struct
        {
            webStreamBuilder.AddPushEvent((serviceProvider) => new PushSignalREvent<TUserKey, TTenantKey>(serviceProvider.GetRequiredService<IHubContext<WebStreamHub<TUserKey, TTenantKey>>>(), serviceProvider.GetRequiredService<IUserConnections>()));
            return webStreamBuilder;
        }
        public static ConnectionManagerBuilder AddSignalR<TUserKey, TTenantKey>(this ConnectionManagerBuilder webStreamBuilder) where TUserKey : struct where TTenantKey : struct
        {
            webStreamBuilder.AddConnectionManager((serviceProvider) => new SignalRConnectionManager<TUserKey, TTenantKey>(serviceProvider.GetRequiredService<IHubContext<WebStreamHub<TUserKey, TTenantKey>>>()));
            return webStreamBuilder;
        }
    }
}
