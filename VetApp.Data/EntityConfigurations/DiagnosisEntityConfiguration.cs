using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Ambulance.ValueObjects;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class DiagnosisEntityConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        public void Configure(EntityTypeBuilder<Diagnosis> diagnosisConfiguration)
        {
            diagnosisConfiguration.ToTable("diagnoses");
            diagnosisConfiguration.HasNoKey();
            diagnosisConfiguration.Property(e => e.DiagnosisName).HasColumnName("name");
        }
    }
}
