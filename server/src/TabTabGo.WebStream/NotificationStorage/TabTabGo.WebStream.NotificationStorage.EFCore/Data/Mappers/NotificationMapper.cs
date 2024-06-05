using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using TabTabGo.Core.Entities;
using TabTabGo.Core.Extensions;
using TabTabGo.Data.EF.Extensions;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Mappers
{
    internal static class NotificationDataMapper
    {
        public static void DataMapperBuilder(this EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("notifications");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(p => p.Id);
            builder.HasQueryFilter(x => x.IsEnabled);
            builder.EntityBuilder<Notification>();

            builder.Property(x => x.Message).HasConversion(
            v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            v => JsonConvert.DeserializeObject<object>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }

}
