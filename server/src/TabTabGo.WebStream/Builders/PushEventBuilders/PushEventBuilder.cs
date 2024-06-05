using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private bool logEnabled = false;
        public PushEventBuilder LogAllOutMessages()
        {
            if (!logEnabled)
            {
                logEnabled = true;
                var oldPushBuilder = _pushBuilder.ToList();
                oldPushBuilder.Insert(0, s => new LogPushEvents(s.GetRequiredService<ILogger<IPushEvent>>()));
                _pushBuilder.Clear();
                _pushBuilder.Add(s => new CompositePushEvent(oldPushBuilder.Select(x => x(s))));
            }
            return this;
        }

        public PushEventBuilder DecorateWith(Func<IServiceProvider, IPushEvent, IPushEvent> func)
        {
            var oldPushBuilder = _pushBuilder.ToList();
            _pushBuilder.Clear();
            _pushBuilder.Add(s => func(s, new CompositePushEvent(oldPushBuilder.Select(x => x(s)))));
            return this;
        }

        public IPushEvent Build(IServiceProvider serviceProvider)
        {
            return new CompositePushEvent(_pushBuilder.Select(x => x(serviceProvider)));
        }
    }
}
