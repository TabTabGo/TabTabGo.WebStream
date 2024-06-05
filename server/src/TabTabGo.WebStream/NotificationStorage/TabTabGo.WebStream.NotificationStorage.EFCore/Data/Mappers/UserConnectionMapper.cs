using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.Data.EF.Extensions;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Mappers
{
    internal static class UserConnectionDataMapper
    {
        public static void DataMapperBuilder(this EntityTypeBuilder<UserConnection> builder)
        {
            builder.ToTable("user_connections");
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasKey(p => p.Id);
            builder.HasQueryFilter(x => x.IsEnabled);
            builder.EntityBuilder<UserConnection>();


        }
    }
}
