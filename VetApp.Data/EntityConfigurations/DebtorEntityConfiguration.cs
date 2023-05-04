using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Finances.Entities;

namespace VDMJasminka.Data.EntityConfigurations
{
    internal class DebtorEntityConfiguration : IEntityTypeConfiguration<Debtor>
    {
        public void Configure(EntityTypeBuilder<Debtor> builder)
        {
            builder.ToTable("debtors");
            builder.HasKey(x => x.Id).HasName("debtors_pkey");
        }
    }
}