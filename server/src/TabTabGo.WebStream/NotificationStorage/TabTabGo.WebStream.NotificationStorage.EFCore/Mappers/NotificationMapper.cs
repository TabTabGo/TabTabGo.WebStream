using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Mappers
{
    internal class NotificationMapper : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        { 
            builder.ToTable("notifications");
            builder.HasKey(m => m.Id); 
        }
    }
}
