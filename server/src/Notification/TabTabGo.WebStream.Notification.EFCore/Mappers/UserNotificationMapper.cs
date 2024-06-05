using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.EFCore.Mappers
{
    internal class UserNotificationMapper : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.ToTable("user_notifications");
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => new { m.UserId, m.NotifiedDateTime, m.Status });
            builder.HasIndex(m => new { m.UserId, m.ReadDateTime });
        }
    }
}
