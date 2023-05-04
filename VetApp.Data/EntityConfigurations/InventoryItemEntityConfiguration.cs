using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class InventoryItemEntityConfiguration : IEntityTypeConfiguration<SupplierOrderItem>
    {
        public void Configure(EntityTypeBuilder<SupplierOrderItem> inventoryItemConfiguration)
        {
            inventoryItemConfiguration.ToTable("supply_reception_details");
            inventoryItemConfiguration.HasKey(e => new { e.SupplierOrderId, e.MedicamentId })
                .HasName("supply_reception_details_pkey");

            inventoryItemConfiguration.HasIndex(e => e.MedicamentId)
                .HasName("medicamentid");

            inventoryItemConfiguration.HasIndex(e => e.SupplierOrderId)
                .HasName("supply_reception_id");

            inventoryItemConfiguration.Property(e => e.SupplierOrderId).HasColumnName("supply_reception_id");

            inventoryItemConfiguration.Property(e => e.MedicamentId).HasColumnName("medicament_id");

            inventoryItemConfiguration.Property(e => e.Price).HasColumnName("price").HasColumnType("double precision");

            inventoryItemConfiguration.Property(e => e.Amount).HasColumnName("amount").HasColumnType("double precision");

            inventoryItemConfiguration.Property(e => e.AdditionalInfo).HasColumnName("notes").HasMaxLength(200);

           
        }
    }
}
