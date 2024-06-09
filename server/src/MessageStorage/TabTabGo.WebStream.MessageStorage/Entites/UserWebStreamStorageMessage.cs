using System;
using System.Collections.Generic;
using TabTabGo.Core.Entities; 

namespace TabTabGo.WebStream.MessageStorage.Entites
{

    public class UserWebStreamStorageMessage : IEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } 
        public DateTime SentDate { get; set; } = DateTime.UtcNow; 
        public Guid MessageId { get; set; }
        public WebStreamStorageMessage Message { get; set; } 
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsEnabled { get; set; } = true;
        public IDictionary<string, object> ExtraProperties { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
