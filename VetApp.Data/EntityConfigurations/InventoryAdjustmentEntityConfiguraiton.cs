using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class InventoryAdjustmentEntityConfiguraiton : IEntityTypeConfiguration<InventoryAdjustment>
    {
        public void Configure(EntityTypeBuilder<InventoryAdjustment> inventoryAdjustmentConfiguration)
        {
            inventoryAdjustmentConfiguration.ToTable("inventory_adjustments");
            inventoryAdjustmentConfiguration.HasKey(e => e.Id).HasName("inventory_adjustments_pkey");
            inventoryAdjustmentConfiguration.Property(e => e.Id).HasColumnName("id").UseIdentityAlwaysColumn();
            inventoryAdjustmentConfiguration.Property(e => e.Date).HasColumnType("date").HasColumnType("timestamp");
            inventoryAdjustmentConfiguration.Property(e => e.Amount).HasColumnName("amount").HasColumnType("double precision");
            inventoryAdjustmentConfiguration.OwnsOne(e => e.Reason).Property(x => x.Value).HasColumnName("reason");
        }
    }
}
