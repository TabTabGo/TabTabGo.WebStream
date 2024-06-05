using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TabTabGo.WebStream.NotificationHub.EFCore.Mappers
{
    internal class NotificationMapper : IEntityTypeConfiguration<NotificationHub.Entities.Notification>
    {
        public void Configure(EntityTypeBuilder<NotificationHub.Entities.Notification> builder)
        { 
            builder.ToTable("notifications");
            builder.HasKey(m => m.Id); 
        }
    }
}
