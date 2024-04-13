using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Notification;

[ExcludeFromCodeCoverage]
public class WebNotificationConfiguration : IEntityTypeConfiguration<WebNotification>
{
    public void Configure(EntityTypeBuilder<WebNotification> builder)
        => builder.Property(messages => messages.Id).ValueGeneratedOnAdd();
}