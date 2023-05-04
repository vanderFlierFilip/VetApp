using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class CheckupEntityConfiguration : IEntityTypeConfiguration<Checkup>
    {
        public void Configure(EntityTypeBuilder<Checkup> checkupConfiguration)
        {
            checkupConfiguration.ToTable("checkups");
            checkupConfiguration.HasIndex(e => e.Id).HasName("PrimaryKey");
            checkupConfiguration.Property(e => e.Id).HasColumnName("id").HasColumnType("counter").ValueGeneratedOnAdd();
            checkupConfiguration.Property(e => e.Date).HasColumnName("date").HasDefaultValueSql("Date()");
            checkupConfiguration.Property(e => e.Diagnosis).HasColumnName("diagnosis").HasMaxLength(255);
            checkupConfiguration.Property(e => e.SurgicalIntervention).HasColumnName("surgical_intervention").HasMaxLength(255);
            checkupConfiguration.Property(e => e.ClinicalPicture).HasColumnName("clinical_picture");
            checkupConfiguration.Property(e => e.Anamnesis).HasColumnName("anamnesis");
            checkupConfiguration.Property(e => e.LabAnalysis).HasColumnName("lab_analisys");
            checkupConfiguration.Property(e => e.NextVaccinationDate).HasColumnName("next_vaccination_date");
            checkupConfiguration.Property(e => e.PaidSum).HasColumnName("paid_sum").HasDefaultValueSql("0");
            checkupConfiguration.Property(e => e.Advice).HasColumnName("medical_advice").HasMaxLength(255);
            checkupConfiguration.Property(e => e.Therapy).HasColumnName("therapy").HasMaxLength(255);
            checkupConfiguration.Property(e => e.Vaccine).HasColumnName("vaccine").HasMaxLength(50);
            checkupConfiguration.Property(e => e.NextControlCheckup).HasColumnName("control_checkup_date");
            checkupConfiguration.Property(e => e.CheckupType).HasColumnName("checkup_type").IsRequired().HasMaxLength(20);
            checkupConfiguration.Property(e => e.PetId).HasColumnName("petid");
            checkupConfiguration.HasOne(d => d.Pet)
                .WithMany(p => p.Checkups)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("PetsPregled");
        }
    }
}
