using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> supplierConfiguration)
        {
            supplierConfiguration.ToTable("suppliers");
            supplierConfiguration.HasKey(x => x.Id).HasName("suppliers_pkey");
            supplierConfiguration.HasMany(x => x.OrderRecievments);
            supplierConfiguration.Property(e => e.Name).HasColumnName("name");
            supplierConfiguration.OwnsOne(x => x.Address).Property(e => e.PhoneNumber).HasColumnName("phone_number");
            supplierConfiguration.OwnsOne(x => x.Address).Property(e => e.Location).HasColumnName("location");
            supplierConfiguration.OwnsOne(x => x.Address).Property(e => e.Address).HasColumnName("street_address");

        }
    }
}
