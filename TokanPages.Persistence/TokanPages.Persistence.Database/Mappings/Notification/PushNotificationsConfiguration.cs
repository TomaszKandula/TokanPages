using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Notification;

[ExcludeFromCodeCoverage]
public class PushNotificationsConfiguration : IEntityTypeConfiguration<PushNotification>
{
    public void Configure(EntityTypeBuilder<PushNotification> builder)
        => builder.Property(notifications => notifications.Id).ValueGeneratedOnAdd();
}