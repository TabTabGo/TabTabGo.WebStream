using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationStorage.EFCore.Mappers;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.EFCore
{
   public class NotificationDbContext(DbContextOptions options) : DbContext(options)
    { 
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Notification>().DataMapperBuilder();
            modelBuilder.Entity<NotificationUser>().DataMapperBuilder();
            modelBuilder.Entity<UserConnection>().DataMapperBuilder();
        }
    }
}
