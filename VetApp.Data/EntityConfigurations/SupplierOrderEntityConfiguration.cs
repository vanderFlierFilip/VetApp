using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class SupplierOrderEntityConfiguration : IEntityTypeConfiguration<SupplierOrder>
    {
        public void Configure(EntityTypeBuilder<SupplierOrder> builder)
        {
            builder.ToTable("supply_receptions");
            builder.HasMany(x => x.Items);
            builder.Property(x => x.InvoiceNumber).HasColumnName("description");
        }
    }
}