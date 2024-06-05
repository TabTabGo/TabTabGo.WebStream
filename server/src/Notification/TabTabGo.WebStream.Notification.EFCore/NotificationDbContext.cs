using Microsoft.EntityFrameworkCore;
using TabTabGo.WebStream.Notification.Entities;

namespace TabTabGo.WebStream.Notification.EFCore
{
    class NotificationDbContext(DbContextOptions options) : DbContext(options)
    { 
        public DbSet<Notification.Entities.NotificationMessage> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
