﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class SubscribersConfiguration : IEntityTypeConfiguration<Subscribers>
    {
        public void Configure(EntityTypeBuilder<Subscribers> AModelBuilder)
            => AModelBuilder.Property(ASubscribers => ASubscribers.Id).ValueGeneratedNever();
    }
}
