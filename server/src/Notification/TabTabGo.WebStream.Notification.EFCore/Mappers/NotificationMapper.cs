using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TabTabGo.WebStream.Notification.EFCore.Mappers
{
    internal class NotificationMapper : IEntityTypeConfiguration<Notification.Entities.NotificationMessage>
    {
        public void Configure(EntityTypeBuilder<Notification.Entities.NotificationMessage> builder)
        { 
            builder.ToTable("notifications");
            builder.HasKey(m => m.Id); 
        }
    }
}
