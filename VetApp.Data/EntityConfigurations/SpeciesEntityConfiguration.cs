using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class SpeciesEntityConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> speciesConfiguration)
        {
            speciesConfiguration.ToTable("species");
            speciesConfiguration.HasKey(e => e.Id).HasName("species_pkey");

            speciesConfiguration.HasIndex(e => e.Id).HasName("VrstaID");

            speciesConfiguration.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("counter");

            speciesConfiguration.Property(e => e.Type).HasColumnName("name").HasMaxLength(25);
        }
    }
}
