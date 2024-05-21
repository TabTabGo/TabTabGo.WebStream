﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabTabGo.WebStream.Builders.EventHandlerBuilders;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Services
{
    public class FirstPassedEventHandler : IReceiveEvent
    {
        List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> _handlers;
        public FirstPassedEventHandler(List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)> handlers)
        {
            _handlers = handlers ?? new List<(Func<WebStreamMessage, bool>, EventHandlerBuilder)>();
        }
        public Task OnEventReceived(string connectionId, WebStreamMessage message)
        {
            var handlerBuilder = _handlers.Where(s => s.Item1(message)).FirstOrDefault(); 
            if (handlerBuilder != default)
            {
                handlerBuilder.Item2.Build().OnEventReceived(connectionId, message);
            }
            return Task.CompletedTask;
        }
    }
}