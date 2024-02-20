using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class ServiceBusMessagesConfiguration : IEntityTypeConfiguration<ServiceBusMessage>
{
    public void Configure(EntityTypeBuilder<ServiceBusMessage> builder) 
        => builder.Property(messages => messages.Id).ValueGeneratedOnAdd();
}