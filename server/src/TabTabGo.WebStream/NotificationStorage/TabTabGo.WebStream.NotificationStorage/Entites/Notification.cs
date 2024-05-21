using System;
namespace TabTabGo.WebStream.NotificationStorage.Entites
{
    public class Notification
    {
        public Guid Id { get; set; }
        public object Message { get; set; }
        public string EventName { get; set; } 
    }
}
