using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using TabTabGo.Core.Entities;
using TabTabGo.Core.Extensions;
using TabTabGo.Data.EF.Extensions;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.EFCore.Mappers
{
    internal static class NotificationDataMapper
    {
        public static void DataMapperBuilder(this EntityTypeBuilder<NotificationMessage> builder)
        {
            builder.ToTable("notifications");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(p => p.Id);
            builder.HasQueryFilter(x => x.IsEnabled);
            builder.EntityBuilder<NotificationMessage>();

            builder.Property(x => x.Message).HasConversion(
            v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            v => JsonConvert.DeserializeObject<object>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }

}
