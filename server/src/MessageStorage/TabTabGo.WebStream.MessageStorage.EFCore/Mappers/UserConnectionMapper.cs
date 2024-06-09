using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.Data.EF.Extensions;
using TabTabGo.WebStream.MessageStorage.Entites;

namespace TabTabGo.WebStream.MessageStorage.EFCore.Mappers
{
    public static class UserConnectionDataMapper
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
