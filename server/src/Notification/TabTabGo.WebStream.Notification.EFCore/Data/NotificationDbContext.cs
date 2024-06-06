using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.Notification.EFCore.Mappers;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.EFCore
{
   public class NotificationDbContext(DbContextOptions options) : DbContext(options)
    { 
        public DbSet<NotificationMessage> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NotificationMessage>().DataMapperBuilder();
            modelBuilder.Entity<NotificationUser>().DataMapperBuilder();
            modelBuilder.Entity<UserConnection>().DataMapperBuilder();
        }
    }
}
