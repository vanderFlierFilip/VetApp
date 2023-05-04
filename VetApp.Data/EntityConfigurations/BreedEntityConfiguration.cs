using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Entities;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class BreedEntityConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> breedConfiguration)
        {
            breedConfiguration.ToTable("breeds");
            breedConfiguration.HasIndex(e => e.AnimalTypeId).HasName("species_idx");
            breedConfiguration.HasIndex(e => e.Id).HasName("breed_id");
            breedConfiguration.HasKey(e => e.Id).HasName("breeds_pkey");

            breedConfiguration.Property(e => e.Id).HasColumnName("id").HasColumnType("counter");
            breedConfiguration.Property(e => e.AnimalTypeId).HasColumnName("species_id").HasColumnType("counter");
            breedConfiguration.Property(e => e.Name).HasColumnName("name").HasMaxLength(50);

            breedConfiguration.HasOne(d => d.Species).WithMany(p => p.Breeds).HasForeignKey(d => d.AnimalTypeId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("VrsteZivotinjaRase");
        }
    }
}
