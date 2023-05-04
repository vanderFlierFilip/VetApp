using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class MedicamentEntityConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> entity)
        {
            entity.ToTable("medicaments");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Id).HasName("idx");
            entity.Property(e => e.Id).HasColumnName("id").HasColumnType("counter");
            entity.OwnsOne(e => e.MedicamentDetails).HasIndex(e => e.Name).HasName("ProductName");
            entity.OwnsOne(e => e.MedicamentDetails).Property(e => e.Name).HasColumnName("name");
            entity.OwnsOne(e => e.MedicamentDetails).Property(e => e.Uom).HasColumnName("uom").HasMaxLength(20);
            entity.OwnsOne(e => e.MedicamentDetails).Property(e => e.IsMaterial).HasColumnName("material").HasColumnType("boolean");
            entity.OwnsOne(e => e.Price).Property(e => e.Value).HasColumnName("price");
            entity.OwnsOne(e => e.MinimalAmount).Property(e => e.Value).HasColumnName("minimal_amount").HasDefaultValueSql("0");
            entity.HasMany(e => e.InventoryAdjustments);

        }
    }
}
