
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.EFCore.Mappers
{
    public class UserWebStreamStorageMessageMapper : IEntityTypeConfiguration<UserWebStreamStorageMessage>
    {
        public void Configure(EntityTypeBuilder<UserWebStreamStorageMessage> builder)
        {
            builder.ToTable("user_stream_messages");
            builder.HasKey(m => m.Id);
            builder.Property(s => s.CreatedBy).IsRequired(false);
            builder.Property(s => s.UpdatedBy).IsRequired(false);
            builder.HasIndex(m => new { m.UserId, m.SentDate });
            builder.HasIndex(s => new { s.UserId, s.TenantId });
        }
    }
}
