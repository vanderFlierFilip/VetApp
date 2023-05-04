using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VDMJasminka.Core.Entities;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class MedicamentInventoryEntityConfiguration : IEntityTypeConfiguration<MedicamentInventory>
    {
        public void Configure(EntityTypeBuilder<MedicamentInventory> medicamentInventoryConfiguration)
        {
            medicamentInventoryConfiguration.ToView("medicamentinventory");
            medicamentInventoryConfiguration.Property(e => e.MedicamentId).HasColumnName("medicament_id");
            medicamentInventoryConfiguration.Property(e => e.SupplierId).HasColumnName("supplier_id");
            medicamentInventoryConfiguration.Property(e => e.MinimalAmount).HasColumnName("min_amount");
            medicamentInventoryConfiguration.Property(e => e.Uom).HasColumnName("uom");
            medicamentInventoryConfiguration.Property(e => e.CurrentAmount).HasColumnName("curr_amount");
            medicamentInventoryConfiguration.HasNoKey();

        }
    }
}
