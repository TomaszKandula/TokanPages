using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Notifications;

namespace TokanPages.Persistence.Database.Mappings.Notifications;

[ExcludeFromCodeCoverage]
public class PushNotificationsConfiguration : IEntityTypeConfiguration<PushNotification>
{
    public void Configure(EntityTypeBuilder<PushNotification> builder)
        => builder.Property(notifications => notifications.Id).ValueGeneratedOnAdd();
}