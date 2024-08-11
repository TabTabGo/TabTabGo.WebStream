﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TabTabGo.WebStream.Model;

namespace TabTabGo.WebStream.Notification.Services
{

    /// <summary>
    /// this interface is used to send notification to user  on "SendNotification" Event and save message to notification database <br/>
  
    /// </summary>
    public interface ISendNotification
    {

        /// <summary>
        /// send message to user on The Event 'SendNotification'
        /// </summary> 
        /// <returns></returns>
        Task SendNotification(IEnumerable<UserIdData> userIds, object data, CancellationToken cancellationToken = default);
        /// <summary>
        /// send message to user on The Event 'SendNotification'
        /// </summary> 
        /// <returns></returns>
        Task SendNotification(UserIdData userId, object data, CancellationToken cancellationToken = default);

    }
}
