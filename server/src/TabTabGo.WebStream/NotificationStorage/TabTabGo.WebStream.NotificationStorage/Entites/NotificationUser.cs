using System;
using TabTabGo.WebStream.NotificationStorage.Entites.Enums;

namespace TabTabGo.WebStream.NotificationStorage.Entites
{

    public class NotificationUser
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public NotificationUserStatus Status { get; set; } = NotificationUserStatus.Unread;
        public DateTime NotidyDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? ReadDateTime { get; set; }
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; }
    }
}
