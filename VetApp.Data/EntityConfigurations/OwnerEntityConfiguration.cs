using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class OwnerEntityConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> ownerConfiguration)
        {
            ownerConfiguration.ToTable("owners");
            ownerConfiguration.HasKey(p => p.Id).HasName("id");
            ownerConfiguration.OwnsOne(e => e.OwnerInformation);
            ownerConfiguration.Ignore(e => e.IsDeleted);

            ownerConfiguration.OwnsOne(p => p.OwnerInformation).HasIndex(e => e.FullName).HasName("full_name");
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.Address).HasColumnName("street_address").HasMaxLength(60);
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.FullName).IsRequired().HasColumnName("name").HasMaxLength(40);
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.SSN).HasColumnName("ssn");
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.Location).HasColumnName("location");
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.Email).HasColumnName("email").HasMaxLength(256);
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.Municipality).HasColumnName("municipality");
            ownerConfiguration.OwnsOne(o => o.OwnerInformation).Property(e => e.AdditionalInfo).HasColumnName("notes");

            ownerConfiguration.OwnsOne(o => o.OwnerInformation)
                .Property(e => e.PhoneNumber).HasColumnName("phone_number")
                .HasMaxLength(24);

            ownerConfiguration.HasIndex(e => e.Id)
                .HasName("CustomerID");

            ownerConfiguration.Property(e => e.Id).HasColumnName("id").HasColumnType("counter");
            ownerConfiguration.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasColumnType("boolean");
        }
    }
}
