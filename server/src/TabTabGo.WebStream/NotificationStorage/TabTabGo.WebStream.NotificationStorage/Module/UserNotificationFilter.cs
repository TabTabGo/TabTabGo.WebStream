using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabTabGo.WebStream.NotificationStorage.Entites.Enums;

namespace TabTabGo.WebStream.NotificationStorage.Module
{
    public class UserNotificationFilter
    {
        public string Q { get; set; }
        public string Status { get; set; }
        public string EventsNames { get; set; }  
        public DateTime? NotifiedDateTimeRangeStart { get; set; }
        public DateTime? NotifiedDateTimeRangeEnd { get; set; } 
        public DateTime? ReadDateRangeStart { get; set; }
        public DateTime? ReadDateRangeEnd { get; set; }

    }
}
