using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;
using TabTabGo.WebStream.Services.Contract;

namespace TabTabGo.WebStream.Services.EventHandlers
{
    internal class LogRecevedEvents : IReceiveEvent
    {
        ILogger<IReceiveEvent> _logger;
        public LogRecevedEvents(ILogger<IReceiveEvent> logger)
        {
            _logger = logger;
        }

        public Task OnEventReceived(string userId, WebStreamMessage message)
        {
            _logger.LogTrace("the user of id {userId}, just sent a message {@message}",userId,message);
            return Task.CompletedTask;
        }
    }
}
