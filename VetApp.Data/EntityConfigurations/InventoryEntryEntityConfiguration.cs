using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class InventoryEntryEntityConfiguration : IEntityTypeConfiguration<SupplierOrder>
    {
        public void Configure(EntityTypeBuilder<SupplierOrder> inventoryEntryConfiguration)
        {
            inventoryEntryConfiguration.ToTable("supply_receptions");
            inventoryEntryConfiguration.HasIndex(e => e.Date)
                .HasName("reception_date");

            inventoryEntryConfiguration.HasIndex(e => e.SupplierId)
                .HasName("supplier_id");

            inventoryEntryConfiguration.HasIndex(e => e.Id)
                .HasName("reception_id");

            inventoryEntryConfiguration.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("counter");
            inventoryEntryConfiguration.Property(e => e.Date).HasColumnName("date");

            inventoryEntryConfiguration.Property(e => e.SupplierId).HasColumnName("supplier_id");

            inventoryEntryConfiguration.Property(e => e.InvoiceNumber).HasColumnName("description").HasMaxLength(50);

        }
    }
}
