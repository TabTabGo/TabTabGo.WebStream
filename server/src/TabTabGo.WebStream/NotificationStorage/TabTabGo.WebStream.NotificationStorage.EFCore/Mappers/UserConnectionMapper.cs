using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Mappers
{
    internal class UserConnectionMapper : IEntityTypeConfiguration<UserConnection>
    {
        public void Configure(EntityTypeBuilder<UserConnection> builder)
        {
            builder.ToTable("user_connections");
            builder.HasKey(m => m.Id);
        }
    }
}
