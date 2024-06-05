
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.EFCore.Mappers
{
    internal class UserWebStreamStorageMessageMapper : IEntityTypeConfiguration<UserWebStreamStorageMessage>
    {
        public void Configure(EntityTypeBuilder<UserWebStreamStorageMessage> builder)
        {
            builder.ToTable("user_stream_messages");
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => new { m.UserId, m.SentDate });
            builder.HasIndex(m => new { m.UserId});
        }
    }
}
