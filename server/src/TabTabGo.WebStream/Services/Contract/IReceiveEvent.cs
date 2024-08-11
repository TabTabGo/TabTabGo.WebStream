﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Services.Contract
{
    /// <summary>
    /// Library client have to use this Interface to handel Event Received from StreamClients
    /// </summary>
    public interface IReceiveEvent
    {
        Task OnEventReceived(UserIdData userId, WebStreamMessage message);
    }
}
