using System;
using System.Collections.Generic;
using System.Linq;
using TabTabGo.WebStream.Services.Contract;
using TabTabGo.WebStream.Services.PushEventsServices;

namespace TabTabGo.WebStream.Builders.PushEventBuilders
{
    public class PushEventBuilder
    {
        private readonly List<Func<IServiceProvider, IPushEvent>> _pushBuilder = new List<Func<IServiceProvider, IPushEvent>>();
        public PushEventBuilder AddPushEvent(Func<IServiceProvider, IPushEvent> func)
        {
            _pushBuilder.Add(func);
            return this;
        }
        public IPushEvent Build(IServiceProvider serviceProvider)
        {
            return new CompositPushEvent(_pushBuilder.Select(x => x(serviceProvider)));
        }
    }
}
