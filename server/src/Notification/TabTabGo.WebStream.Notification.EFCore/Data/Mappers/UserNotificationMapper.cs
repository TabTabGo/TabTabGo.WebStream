using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.Data.EF.Extensions;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.EFCore.Mappers
{
    public static class NotificationUserDataMapper
    {
        public static void DataMapperBuilder(this EntityTypeBuilder<NotificationUser> builder)
        {
            builder.ToTable("user_notifications");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(p => p.Id);
            builder.HasQueryFilter(x => x.IsEnabled);
            builder.EntityBuilder<NotificationUser>();
            builder.Property(s => s.CreatedBy).IsRequired(false);
            builder.Property(s => s.UpdatedBy).IsRequired(false);
            builder.HasIndex(m => new { m.UserId, m.NotifiedDateTime, m.Status });
            builder.HasIndex(m => new { m.UserId, m.ReadDateTime });
        }
    }
}
