using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class SupplierOrderItemEntityConfiguration : IEntityTypeConfiguration<SupplierOrderItem>
    {
        public void Configure(EntityTypeBuilder<SupplierOrderItem> builder)
        {
            builder.ToTable("supply_reception_details");
            builder.Property(x => x.AdditionalInfo).HasColumnName("notes");
            builder.Property(x => x.SupplierOrderId).HasColumnName("supply_reception_id");
        }
    }
}