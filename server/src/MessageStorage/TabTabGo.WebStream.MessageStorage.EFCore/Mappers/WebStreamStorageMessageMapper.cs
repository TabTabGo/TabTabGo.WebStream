

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.EFCore.Mappers
{
    internal class WebStreamStorageMessageMapper : IEntityTypeConfiguration<WebStreamStorageMessage>
    {
        public void Configure(EntityTypeBuilder<WebStreamStorageMessage> builder)
        { 
            builder.ToTable("stream_messages");
            builder.HasKey(m => m.Id); 
        }
    }
}
