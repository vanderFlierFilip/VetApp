using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> petConfiguration)
        {
            petConfiguration.ToTable("pets");
            petConfiguration.HasKey(p => p.Id);
            petConfiguration.OwnsOne(p => p.Chip);
            petConfiguration.HasMany(p => p.Checkups);
            petConfiguration.HasIndex(e => e.Id)
                .HasName("Id index");

            petConfiguration.HasIndex(e => e.SpeciesId)
                 .HasName("speciesId");

            petConfiguration.HasIndex(e => new { e.SpeciesId, e.BreedId })
                .HasName("Index1");

            petConfiguration.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("counter");
            petConfiguration.Property(x => x.OwnerId).HasColumnName("owner_id");

            petConfiguration.Property(e => e.SpeciesId).HasColumnName("species_id");
            petConfiguration.Property(e => e.BreedId).HasColumnName("breed_id");
            petConfiguration.Property(d => d.LastVisit).HasColumnName("last_visit");

            petConfiguration.OwnsOne(p => p.Chip).Property(e => e.Number).HasColumnName("chip_number");
            petConfiguration.OwnsOne(p => p.PetInformation).Property(e => e.Alergy).HasColumnName("alergy").HasMaxLength(50);
            petConfiguration.OwnsOne(p => p.PetInformation).Property(e => e.Name).HasColumnName("name")
                .IsRequired()
                .HasMaxLength(20);
            petConfiguration.OwnsOne(p => p.PetInformation).Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            petConfiguration.OwnsOne(p => p.PetInformation).Property(e => e.Sex).HasColumnName("sex").HasMaxLength(1);

            petConfiguration.OwnsOne(p => p.PetInformation).Property(x => x.Description).HasColumnName("description");

            petConfiguration.HasOne(d => d.Species)
                .WithMany(p => p.Pets)
                .HasForeignKey(d => d.SpeciesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VrsteZivotinjaPets");
            petConfiguration.HasOne(p => p.Breed);

            petConfiguration.HasMany(d => d.Checkups).WithOne(x => x.Pet);
        }
    }
}