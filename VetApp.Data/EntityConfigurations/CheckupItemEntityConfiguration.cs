using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class CheckupItemEntityConfiguration : IEntityTypeConfiguration<CheckupItem>
    {
        public void Configure(EntityTypeBuilder<CheckupItem> checkupItemConfiguration)
        {
            checkupItemConfiguration.ToTable("checkup_details");
            checkupItemConfiguration.HasKey(e => e.Id).HasName("checkup_details_pkey");
            checkupItemConfiguration.HasIndex(e => e.MedicamentId).HasName("medid");
            checkupItemConfiguration.HasIndex(e => e.Id)
                .HasName("checkup_details_id")
                .IsUnique();
            checkupItemConfiguration.HasIndex(e => e.CheckupId)
                .HasName("Visit DetailsVisit Number");

            checkupItemConfiguration.Property(e => e.CheckupId).HasColumnName("checkup_id");

            checkupItemConfiguration.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("counter")
                .ValueGeneratedOnAdd();

            checkupItemConfiguration.Property(e => e.MedicamentApplication).HasColumnName("application").HasMaxLength(2);
            checkupItemConfiguration.Property(e => e.Price).HasColumnName("price").HasDefaultValueSql("0");
            checkupItemConfiguration.Property(e => e.Uom).HasColumnName("uom").HasMaxLength(50);
            checkupItemConfiguration.Property(e => e.Amount).HasColumnName("amount").HasDefaultValueSql("0");
            checkupItemConfiguration.Property(e => e.MedicamentId).HasColumnName("medicament_id");
            
            checkupItemConfiguration.HasOne(d => d.Checkup)
                .WithMany(p => p.CheckupItems)
                .HasForeignKey(d => d.CheckupId);
        }
    }
}
