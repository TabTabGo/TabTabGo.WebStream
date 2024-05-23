using System;

namespace TabTabGo.WebStream.Model
{
    public class WebStreamMessage
    {
        private readonly Guid notificationId = Guid.NewGuid();
        public Guid NotificationId => notificationId;
        public string EventName { get; set; }
        public object Data { get; set; }
    }
}
