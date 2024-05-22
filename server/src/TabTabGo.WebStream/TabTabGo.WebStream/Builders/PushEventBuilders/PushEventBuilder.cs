using System;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Builders.PushEventBuilders
{
    public class PushEventBuilder
    {
        Func<IServiceProvider, IPushEvent> pushBuilder;
        public PushEventBuilder SetIPushEvent(Func<IServiceProvider, IPushEvent> func)
        {
            pushBuilder = func;
            return this;
        }
        public IPushEvent Build(IServiceProvider serviceProvider)
        {
            return pushBuilder(serviceProvider); 
        }
    }
}
