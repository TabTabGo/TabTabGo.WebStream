﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabTabGo.WebStream.NotificationStorage.Entites;

namespace TabTabGo.WebStream.NotificationStorage.EFCore.Mappers
{
    internal class UserNotificationMapper : IEntityTypeConfiguration<NotificationUser>
    {
        public void Configure(EntityTypeBuilder<NotificationUser> builder)
        {
            builder.ToTable("tabtabgo_user_notifications");
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => new { m.UserId, m.NotifiedDateTime, m.Status });
            builder.HasIndex(m => new { m.UserId, m.ReadDateTime });
        }
    }
}