using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using TabTabGo.WebStream.Builders;
using TabTabGo.WebStream.Services;
using TabTabGo.WebStream.SignalR.Hub;

namespace TabTabGo.WebStream.Builders
{
    public static class SignalRWebStreamBuilder
    {
        public static WebStreamBuilder UseSignalR(this WebStreamBuilder webStreamBuilder, IServiceProvider serviceProvider)
        {
            webStreamBuilder.SetEventSender(() => new PushSignalREvent(serviceProvider.GetRequiredService<IHubContext<TabtabGoHub>>()));
            return webStreamBuilder;
        }
    }
}
