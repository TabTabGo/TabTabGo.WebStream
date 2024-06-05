using System;
using System.Collections.Generic;
using TabTabGo.Core.Entities;
using TabTabGo.WebStream.Notification.Entities.Enums;

namespace TabTabGo.WebStream.Notification.Entities
{

    public class NotificationUser : IEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public NotificationUserStatus Status { get; set; } = NotificationUserStatus.Unread;
        public DateTime NotifiedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? ReadDateTime { get; set; }
        public Guid NotificationId { get; set; }
        public NotificationMessage NotificationMessage { get; set; }

        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsEnabled { get; set; }
        public IDictionary<string, object> ExtraProperties { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
