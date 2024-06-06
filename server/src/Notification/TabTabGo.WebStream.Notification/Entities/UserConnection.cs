using System;
using System.Collections.Generic;
using TabTabGo.Core.Entities;
namespace TabTabGo.WebStream.Notification.Entities
{
    public class UserConnection : IEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTimeOffset? ReConnectedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsEnabled { get; set; }
        public IDictionary<string, object> ExtraProperties { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
