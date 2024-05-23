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

        public PushEventBuilder DecorateWith(Func<IServiceProvider, IPushEvent, IPushEvent> func)
        {
            var oldPushBuilder = _pushBuilder.ToList();
            _pushBuilder.Clear();
            _pushBuilder.Add(s => func(s, new CompositPushEvent(oldPushBuilder.Select(x => x(s)))));
            return this;
        }

        public IPushEvent Build(IServiceProvider serviceProvider)
        {
            return new CompositPushEvent(_pushBuilder.Select(x => x(serviceProvider)));
        }
    }
}
