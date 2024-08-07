using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using TabTabGo.Core.Entities;
using TabTabGo.Core.Extensions;
using TabTabGo.Data.EF.Extensions;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.EFCore.Mappers
{
    public static class NotificationDataMapper
    {
        public static void DataMapperBuilder(this EntityTypeBuilder<NotificationMessage> builder)
        {
            builder.ToTable("notifications");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(p => p.Id);
            builder.Property(s => s.CreatedBy).IsRequired(false);
            builder.Property(s => s.UpdatedBy).IsRequired(false);
            builder.HasQueryFilter(x => x.IsEnabled);
            builder.EntityBuilder<NotificationMessage>();

            builder.Property(x => x.Message).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<dynamic>(v, new JsonSerializerOptions { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull }));
        }
    }

}
