using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.NotificationHub.Entities;

namespace TabTabGo.WebStream.NotificationHub.EFCore
{
    class NotificationDbContext(DbContextOptions options) : DbContext(options)
    { 
        public DbSet<NotificationHub.Entities.Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
